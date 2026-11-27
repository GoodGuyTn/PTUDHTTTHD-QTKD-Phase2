using Microsoft.EntityFrameworkCore;
using QTKD.Application.Abstractions;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence;

namespace QTKD.Infrastructure.Repositories;

public class ContractRepository(QtkdDbContext db) : IContractRepository
{
    public Task<Contract?> GetByIdAsync(int id, CancellationToken ct = default) =>
        db.Contracts.AsNoTracking()
            .Include(c => c.Customer)   // JOIN Customers để lấy tên khách hàng
            .FirstOrDefaultAsync(c => c.Id == id, ct);
}
