using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.Common.Abstractions.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand;

public interface IBaseCommand;
public interface ITransactionalCommand;