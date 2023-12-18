using Domain.ErrorHandlers;
using MediatR;

namespace Application.Abstractions.Messages;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}