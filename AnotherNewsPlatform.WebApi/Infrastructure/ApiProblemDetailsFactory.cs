using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

public class ApiProblemDetailsFactory
{
     public static ProblemDetails? Create(HttpContext httpContext,
        int statusCode,
        string title,
        string? detail,
        Exception? exception) => Create(httpContext, statusCode, title, detail, exception, null);

    public static ProblemDetails? Create(HttpContext httpContext,
        int statusCode,
        string title,
        string? detail) => Create(httpContext, statusCode, title, detail, null, null);

    public static ProblemDetails? Create(HttpContext httpContext,
        int statusCode,
        string title,
        string? detail,
        Dictionary<string, string[]>? validationErrors) => Create(httpContext, statusCode, title, detail, null, validationErrors);

    public static ProblemDetails? Create(HttpContext httpContext, 
        int statusCode, 
        string title, 
        string? detail, 
        Exception? exception,
        Dictionary<string, string[]>? validationErrors)
    {
        var problemDetails = validationErrors is not null
        ? new ValidationProblemDetails(validationErrors)
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        }
        :
         new ProblemDetails
         {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        var environment = httpContext.RequestServices.GetService<IHostEnvironment>();
        if (environment?.IsDevelopment() == true && exception != null)
        {
            problemDetails.Extensions["exception"] = exception.GetType().FullName;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;

        }
        return problemDetails;
    }

    public static ObjectResult CreateResponse(
        HttpContext httpContext,
        int statusCode,
        string title,
        string? detail,
        Exception? exception)
    {
        var problemDetails = Create(httpContext, statusCode, title, detail, exception);
        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }

    public static ObjectResult CreateResponse(
        HttpContext httpContext,
        int statusCode,
        string title,
        string? detail)
    {
        var problemDetails = CreateResponse(httpContext, statusCode, title, detail, null);
        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }

    public static ObjectResult NotFound(
        HttpContext httpContext,
        string detail)=>  CreateResponse(httpContext, StatusCodes.Status404NotFound, "Not Found", detail);

    public static ObjectResult BadRequest(
        HttpContext httpContext,
        string detail) => CreateResponse(httpContext, StatusCodes.Status400BadRequest, "Bad Request", detail);

        public static ObjectResult InternalServerError(
            HttpContext httpContext,
            string detail, 
            Exception? exception) => CreateResponse(httpContext, StatusCodes.Status500InternalServerError, "Internal Server Error", detail, exception);

}