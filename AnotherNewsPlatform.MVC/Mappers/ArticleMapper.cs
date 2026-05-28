using AnotherNewsPlatform.MVC.Models.Articles;
using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.Database.Entities;
using AnotherNewsPlatform.Database;

namespace AnotherNewsPlatform.MVC.Mappers
{
    [Mapper]
    public partial class ArticleMapper
    {
        public partial ArticleDto ToDto(CreateArticleModel model);
        public partial ArticlePreviewModel ToPreviewModel(ArticleDto articleDto);

        [MapperIgnoreSource(nameof(ArticleDto.SourceId))]
        [MapperIgnoreSource(nameof(ArticleDto.Content))]
        //[MapperIgnoreSource(nameof(ArticleDto.OriginalUrl))]
        [MapperIgnoreTarget(nameof(ArticleViewModel.Source))]
        public partial ArticleViewModel ToViewModel(ArticleDto dto);
    }
}
