using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.MVC.Models;

namespace AnotherNewsPlatform.MVC.Mappers.Articles;

public partial class NewsDtoToArticlePreviewHandler
{
    public partial ArticlePreviewModel ToArticlePreview(ArticleDto dto);
}
