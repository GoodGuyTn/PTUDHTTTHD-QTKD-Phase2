using Microsoft.EntityFrameworkCore;
using QTKD.Application.Abstractions;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence;

namespace QTKD.Infrastructure.Repositories;

public class CustomerRepository(QtkdDbContext db) : ICustomerRepository
{
    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken ct = default) =>
        // AsNoTracking: chỉ đọc, không cần EF theo dõi thay đổi → nhanh hơn.
        await db.Customers.AsNoTracking().OrderBy(c => c.Id).ToListAsync(ct);
}
