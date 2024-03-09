using Kollity.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResult> : IRequest<Result<TResult>>, ICommand
{
}

public interface ICommandWithEvents : ICommand
{
}

public interface ICommandWithEvents<TResult> : ICommand<TResult>, ICommandWithEvents
{
}