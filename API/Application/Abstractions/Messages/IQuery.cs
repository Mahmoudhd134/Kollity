using Domain.ErrorHandlers;
using MediatR;

namespace Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}