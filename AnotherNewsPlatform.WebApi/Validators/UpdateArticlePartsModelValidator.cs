using System.Data;
using AnotherNewsPlatform.WebApi.Models;
using FluentValidation;

namespace AnotherNewsPlatform.WebApi.Validators;

public class UpdateArticlePartsModelValidator: AbstractValidator<UpdateArticlePartsModel>
{
    public UpdateArticlePartsModelValidator()
    {
        RuleFor(model => model)
            .Must(model => model.Title is not null || model.Rate.HasValue)
            .WithMessage("At least title or rate must be provided!");

        When(model => model.Rate is not null, () =>
        {
            RuleFor(model => model.Rate)
                .InclusiveBetween(-10, 10)
                .WithMessage("Rate must be between -10 and 10!");
        } );
    }
}