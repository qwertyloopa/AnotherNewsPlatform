using AnotherNewsPlatform.MVC.Models.Articles;
using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.MVC.Mappers
{
    [Mapper]
    public partial class ArticleMapper
    {
        public partial ArticleDto ToDto(CreateArticleModel model);

        public partial ArticlePreviewModel ToPreviewModel(ArticleDto articleDto);
    }
}
