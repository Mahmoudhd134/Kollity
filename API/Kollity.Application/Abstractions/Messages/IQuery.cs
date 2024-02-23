using Kollity.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}