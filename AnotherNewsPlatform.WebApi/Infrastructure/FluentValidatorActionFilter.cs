using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

public sealed class FluentValidatorActionFilter(IOptions<ApiBehaviorOptions> apiBehaviorOptions): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var(argName, argValue) in context.ActionArguments)
        {
            if(argValue is null) continue;
            
            var validatorType = typeof(IValidator<>).MakeGenericType(argValue.GetType());
            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
            {
                continue;
            }
            
            var validationContext = new ValidationContext<object>(argValue);
            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            foreach (var error in validationResult.Errors)
            {
                var key = string.IsNullOrEmpty(error.PropertyName) ? argName : $"{argName}:{argValue}";
                context.ModelState.AddModelError(key, error.ErrorMessage);
            }

            if (context.ModelState.IsValid)
            {
                context.Result = apiBehaviorOptions.Value.InvalidModelStateResponseFactory(context);
                return;
            }
            
            await next();
        }
    }
}