using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.MVC.Models.Articles;

namespace AnotherNewsPlatform.MVC.Mappers.Articles;

[Mapper]
public partial class DtoToArticlePreviewMapper
{
    [MapperIgnoreSource(nameof(ArticleDto.SourceId))]
    public partial ArticlePreviewModel Map(ArticleDto dto);

}
