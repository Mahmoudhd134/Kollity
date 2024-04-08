using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Services.Application.Abstractions.Messages;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}