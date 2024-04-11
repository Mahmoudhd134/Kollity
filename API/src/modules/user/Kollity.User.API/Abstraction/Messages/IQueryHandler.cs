using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.User.API.Abstraction.Messages;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery<TResult>
{
}