using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using MediatR;

namespace Kollity.Services.Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}