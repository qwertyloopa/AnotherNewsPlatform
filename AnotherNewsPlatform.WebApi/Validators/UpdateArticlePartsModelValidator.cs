using System.Data;
using AnotherNewsPlatform.WebApi.Models;
using FluentValidation;

namespace AnotherNewsPlatform.WebApi.Validators;

public class UpdateArticlePartsModelValidator: AbstractValidator<UpdateArticlePartsModel>
{
    public UpdateArticlePartsModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        RuleFor(model => model.Rate)
            .InclusiveBetween(0, 10)
            .WithMessage("Rate must be between 0 and 10");
    }
}