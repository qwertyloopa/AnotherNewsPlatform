using System;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleById(AnpDbContext dbContext, Guid id) : IRequest<ArticleDto>;

