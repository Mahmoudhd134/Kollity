using Kollity.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>
{
}

<<<<<<< HEAD
public interface ICommand<TResult> : IRequest<Result<TResult>>
=======
public interface ICommand<TResult> : IRequest<Result<TResult>>, ICommand
{
}

public interface ICommandWithEvents : ICommand
{
}

public interface ICommandWithEvents<TResult> : ICommand<TResult>, ICommandWithEvents
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
{
}