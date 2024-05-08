using Kollity.Services.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kollity.Services.Application.MediatorPipelines;

public class HandelTransactionsPipeline<TRequest, TResponse>(
    ApplicationDbContext context,
    ILogger<HandelTransactionsPipeline<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        TResponse response = null;
        try
        {
            response = await next();
            if (response.IsSuccess)
            {
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.GetErrorMessage());
        }

        await transaction.RollbackAsync(cancellationToken);
        return response;
    }
}