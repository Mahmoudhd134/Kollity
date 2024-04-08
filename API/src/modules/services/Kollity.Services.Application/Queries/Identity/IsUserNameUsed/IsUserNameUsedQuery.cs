using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Queries.Identity.IsUserNameUsed;

public record IsUserNameUsedQuery(string UserName) : IQuery<bool>;