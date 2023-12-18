namespace Application.Queries.Identity.IsEmailUsed;

public record IsEmailUsedQuery(string Email) : IQuery<bool>;