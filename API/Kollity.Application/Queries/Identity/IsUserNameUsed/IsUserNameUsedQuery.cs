﻿namespace Kollity.Application.Queries.Identity.IsUserNameUsed;

public record IsUserNameUsedQuery(string UserName) : IQuery<bool>;