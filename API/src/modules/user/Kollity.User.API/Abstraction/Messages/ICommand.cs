using Kollity.Common.ErrorHandling;
using MediatR;

namespace Kollity.User.API.Abstraction.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand;

public interface IBaseCommand;