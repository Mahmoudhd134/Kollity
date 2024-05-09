using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.Common.Abstractions.Messages;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}