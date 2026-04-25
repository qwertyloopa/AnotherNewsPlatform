using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.MVC.Models;

namespace AnotherNewsPlatform.MVC.Mappers.Articles;

[Mapper]
public partial class NewsDtoToArticlePreview
{
    [MapperIgnoreSource(nameof(ArticleDto.SourceId))]
    public partial ArticlePreviewModel ToArticlePreview(ArticleDto dto);
}
