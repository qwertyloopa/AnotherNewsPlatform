using AnotherNewsPlatform.App.Models;
using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.MVC.Mappers.Articles
{
    [Mapper]
    public partial class CreateArticleModelToDtoMapper
    {
        public partial ArticleDto ToArticleDto(CreateArticleModel model);
    }
}
