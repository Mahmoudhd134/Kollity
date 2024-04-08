using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Services.Application.Abstractions.Messages;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}