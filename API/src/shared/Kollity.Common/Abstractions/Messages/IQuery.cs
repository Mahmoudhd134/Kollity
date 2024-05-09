using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.Common.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}