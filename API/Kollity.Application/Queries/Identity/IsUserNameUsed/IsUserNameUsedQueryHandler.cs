using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Identity.IsUserNameUsed;

public class IsUserNameUsedQueryHandler : IQueryHandler<IsUserNameUsedQuery, bool>
{
    private readonly ApplicationDbContext _context;

    public IsUserNameUsedQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result<bool>> Handle(IsUserNameUsedQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(
            u => u.NormalizedUserName == request.UserName.ToUpper(), cancellationToken);
    }
}