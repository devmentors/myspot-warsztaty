using System.Diagnostics;

namespace MySpot.Api.Middlewares;

public class LoggingMiddleware : IMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Started executing the request with ID: {TraceId}...", context.TraceIdentifier);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await next(context);
        stopwatch.Stop();
        var time = stopwatch.ElapsedMilliseconds;
        _logger.LogInformation("Finished executing the request with ID: {TraceId} in {RequestTime} ms.",
            context.TraceIdentifier, time);
    }
}