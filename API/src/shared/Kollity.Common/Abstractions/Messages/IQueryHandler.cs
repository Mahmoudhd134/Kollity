using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.Common.Abstractions.Messages;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}