using Kollity.Services.Application.Abstractions.Events;
using MediatR;

namespace Kollity.Services.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand;

public interface IBaseCommand;
public interface ITransactionalCommand;