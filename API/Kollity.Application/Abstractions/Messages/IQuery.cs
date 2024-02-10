using MediatR;

namespace Kollity.Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}