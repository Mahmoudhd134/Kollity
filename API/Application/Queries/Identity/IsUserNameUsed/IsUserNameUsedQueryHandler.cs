using Domain.ErrorHandlers;
using Domain.Identity.User;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Queries.Identity.IsUserNameUsed;

public class IsUserNameUsedQueryHandler : IQueryHandler<IsUserNameUsedQuery, bool>
{
    private readonly IUserRepository _userRepository;

    public IsUserNameUsedQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<bool>> Handle(IsUserNameUsedQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUserNameUsed(request.UserName, cancellationToken);
    }
}