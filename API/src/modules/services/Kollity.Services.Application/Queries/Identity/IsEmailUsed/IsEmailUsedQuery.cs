using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Queries.Identity.IsEmailUsed;

public record IsEmailUsedQuery(string Email) : IQuery<bool>;