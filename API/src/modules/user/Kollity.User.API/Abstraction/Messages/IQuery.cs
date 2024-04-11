using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.User.API.Abstraction.Messages;

public interface IQuery<TResult> : IRequest<Result>
{
}