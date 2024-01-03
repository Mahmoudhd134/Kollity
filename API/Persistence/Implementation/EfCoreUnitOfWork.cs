using Persistence.Abstractions;
using Persistence.Data;

namespace Persistence.Implementation;

public class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EfCoreUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}