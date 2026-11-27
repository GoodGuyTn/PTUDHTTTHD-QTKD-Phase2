# QTKD — Hệ thống Quản trị Kinh doanh (TH Phase 2)

Backend quản lý **Khách hàng – Hợp đồng – Bảng giá** cho Công ty ABC (logistics/cảng), xây dựng dần qua 3 tuần:

| Tuần | Nội dung | Trạng thái |
|---|---|---|
| 05 | Solution phân lớp, entity, `QtkdDbContext`, migration, seed, `GET /api/customers`, `GET /api/contracts/{id}` | ✅ |
| 06 | CRUD Customer/Contract/PriceList, `POST /api/contracts/{id}/submit` (US03, CTR-01..03) | ⏳ |
| 07 | xUnit: unit test + integration test (TC01–TC08) | ⏳ |

**Công nghệ:** .NET 10 · ASP.NET Core Web API · EF Core 10 · PostgreSQL 16 (Docker) · Swagger.

## Cấu trúc thư mục

```
BaiTapTH02/
├── src/
│   ├── QTKD.Domain/          # Entity + enum, không phụ thuộc gì
│   ├── QTKD.Application/     # Service nghiệp vụ, DTO, interface Repository, exception
│   ├── QTKD.Infrastructure/  # QtkdDbContext, cấu hình EF, seed, migration, Repository
│   └── QTKD.Api/             # Controller, middleware lỗi, Program.cs (composition root)
├── tests/                    # (tuần 07)
├── docs/                     # Sơ đồ thành phần, ảnh minh chứng
├── docker-compose.yml        # PostgreSQL + Adminer
├── .env.example              # Mẫu biến môi trường (copy thành .env)
└── QTKD.slnx
```

Sơ đồ thành phần và luồng phụ thuộc: [docs/architecture.md](docs/architecture.md).

## Yêu cầu cài đặt

- .NET SDK 10
- Docker Desktop
- Công cụ EF Core: `dotnet tool install --global dotnet-ef`

## 1. Cấu hình cơ sở dữ liệu

Mật khẩu **không** được lưu trong mã nguồn. Có hai chỗ cần mật khẩu, cả hai đều nằm ngoài Git:

**a) Mật khẩu cho container PostgreSQL** — file `.env` (đã nằm trong `.gitignore`):

```powershell
Copy-Item .env.example .env
# Mở .env và đổi POSTGRES_PASSWORD
```

**b) Chuỗi kết nối cho API** — lưu bằng [user-secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) (lưu trong hồ sơ người dùng Windows, không nằm trong thư mục dự án):

```powershell
dotnet user-secrets set "ConnectionStrings:QtkdDb" "Host=localhost;Port=5433;Database=qtkd;Username=qtkd;Password=<mật khẩu trong .env>" --project src/QTKD.Api
```

Cách khác (ví dụ trên máy CI): đặt biến môi trường `ConnectionStrings__QtkdDb` với cùng giá trị.

Khởi động PostgreSQL và Adminer:

```powershell
docker compose up -d
```

PostgreSQL chạy ở cổng **5433** (tránh trùng Postgres khác ở 5432). Adminer ở http://localhost:8081.

## 2. Chạy migration

Migration nằm trong `src/QTKD.Infrastructure/Persistence/Migrations`. Lệnh sau tạo bảng **và** nạp dữ liệu mẫu:

```powershell
dotnet ef database update --project src/QTKD.Infrastructure --startup-project src/QTKD.Api
```

Khi thay đổi entity, tạo migration mới:

```powershell
dotnet ef migrations add <TenMigration> --project src/QTKD.Infrastructure --startup-project src/QTKD.Api --output-dir Persistence/Migrations
```

### Dữ liệu mẫu (seed)

Khai báo trong `src/QTKD.Infrastructure/Persistence/Seed/SeedData.cs`, đưa vào migration bằng `HasData`:

| Bảng | Dữ liệu |
|---|---|
| Customers | 1 Công ty Minh Phát (Active), 2 Công ty TNHH Hải Long (Active), 3 Công ty CP Vận tải Sao Mai (Inactive) |
| Contracts | 1 `HD-2026-018` — Minh Phát — 01/10/2026 → 30/09/2027 — **Draft** |
| PriceLists | 1 Bảng giá Port Services 2026 — áp dụng từ 01/10/2026 |
| PriceItems | (1, `PS-LOAD`, 1.250.000) · (1, `PS-STORAGE`, 85.000) |

Sequence Id của các bảng bắt đầu từ 100 để bản ghi tạo mới qua API không trùng Id với dữ liệu seed.

## 3. Chạy API

```powershell
dotnet run --project src/QTKD.Api
```

API chạy tại http://localhost:5080. Swagger UI mở ngay ở trang gốc http://localhost:5080/.

## 4. Thử endpoint

| Method | URL | Kết quả mong đợi |
|---|---|---|
| GET | `/api/customers` | 200 — danh sách 3 khách hàng |
| GET | `/api/contracts/1` | 200 — hợp đồng HD-2026-018, `status: "Draft"` |
| GET | `/api/contracts/999` | 404 — ProblemDetails "Không tìm thấy hợp đồng có Id = 999." |

```powershell
Invoke-RestMethod http://localhost:5080/api/customers
```

```powershell
Invoke-RestMethod http://localhost:5080/api/contracts/1
```

Mẫu phản hồi `GET /api/contracts/1`:

```json
{
  "id": 1,
  "contractNo": "HD-2026-018",
  "customerId": 1,
  "customerName": "Công ty Minh Phát",
  "startDate": "2026-10-01",
  "endDate": "2027-09-30",
  "status": "Draft"
}
```

Mẫu lỗi `GET /api/contracts/999` (`application/problem+json`):

```json
{
  "title": "Not Found",
  "status": 404,
  "detail": "Không tìm thấy hợp đồng có Id = 999.",
  "instance": "/api/contracts/999"
}
```

## 5. Xem dữ liệu trong PostgreSQL

- **Adminer:** http://localhost:8081 → System `PostgreSQL`, Server `postgres`, Username `qtkd`, Password (trong `.env`), Database `qtkd`.
- **Dòng lệnh** (không cần cài `psql` trên máy). Câu SQL được đưa vào qua stdin vì Windows PowerShell 5.1 làm mất dấu `"` khi truyền tham số cho chương trình ngoài:

```powershell
'SELECT * FROM "Contracts";' | docker exec -i qtkd-postgres psql -U qtkd -d qtkd
```

## Ảnh minh chứng

Ảnh nằm trong [docs/screenshots](docs/screenshots).
