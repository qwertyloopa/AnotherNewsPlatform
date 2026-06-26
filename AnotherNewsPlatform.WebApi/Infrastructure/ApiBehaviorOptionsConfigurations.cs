using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

public static class ApiBehaviorOptionsConfigurations
{
   public static void Configure(ApiBehaviorOptions options)
   {
      options.InvalidModelStateResponseFactory = context =>
      {
         var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

         var problemDetails = ApiProblemDetailsFactory.Create(context.HttpContext,
            StatusCodes.Status400BadRequest,
            "Validation Error",
            "One or more validation errors occurred.",
            errors);
         return new BadRequestObjectResult(problemDetails);
      };
   }
}