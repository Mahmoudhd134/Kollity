using Domain.ErrorHandlers;
using Domain.Identity.User;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Queries.Identity.IsEmailUsed;

public class IsEmailUsedQueryHandler : IQueryHandler<IsEmailUsedQuery, bool>
{
    private readonly IUserRepository _userRepository;

    public IsEmailUsedQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(IsEmailUsedQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.IsEmailUsed(request.Email, cancellationToken);
    }
}