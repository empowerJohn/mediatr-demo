using MediatR;

namespace MyMediatR.API.Application.Behaviors;

public class MyLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public MyLoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            _logger.LogInformation($"Before execution [{typeof(TRequest).Name}]");

            return await next(); // executes the request
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while executing request for [{typeof(TRequest).Name}]: {ex.Message}");
            throw;
        }
        finally
        {
            _logger.LogInformation($"After execution [{typeof(TRequest).Name}]");
        }
    }
}