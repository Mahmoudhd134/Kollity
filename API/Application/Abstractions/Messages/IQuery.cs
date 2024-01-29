using MediatR;

namespace Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}