using MediatR;

namespace Kollity.Services.Application.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}