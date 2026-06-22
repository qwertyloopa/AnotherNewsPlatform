using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

public sealed class FluentValidatorActionFilter(IOptions<ApiBehaviorOptions> apiBehaviorOptions): IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var(argName, argValue) in context.ActionArguments)
        {
            if(argValue is null) continue;
            var validatorType = typeof(IValidator<>).MakeGenericType(argValue.GetType());
        }
    }
}