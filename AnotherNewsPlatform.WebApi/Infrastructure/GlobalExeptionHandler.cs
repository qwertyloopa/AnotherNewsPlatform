using AnotherNewsPlatform.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

public class GlobalExeptionHandler(ILogger<GlobalExeptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = MapException(exception);
        
        if  (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        else logger.LogError(exception, "An handled exception occurred: {Message}", exception.Message);
        
        var problemDetails = ApiProblemDetailsFactory.Create(httpContext, statusCode, title, exception.Message, exception);
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }

    public static (int statusCode, string title) MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Requested resource was not found."),
            BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request."),
            AlreadyExistsException => (StatusCodes.Status409Conflict, "Resource already exists."),
            InternalServerErrorException => (StatusCodes.Status500InternalServerError, "Internal server error."),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error.")
        };
    }
}