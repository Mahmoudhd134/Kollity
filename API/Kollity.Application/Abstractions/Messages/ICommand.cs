using Kollity.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand;

public interface ICommandWithEvents : ICommand, IBaseCommandWithEvents;

public interface ICommandWithEvents<TResult> : ICommand<TResult>, IBaseCommandWithEvents;

public interface IBaseCommand;

public interface IBaseCommandWithEvents;