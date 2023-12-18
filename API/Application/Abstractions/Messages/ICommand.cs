using Domain.ErrorHandlers;
using MediatR;

namespace Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}