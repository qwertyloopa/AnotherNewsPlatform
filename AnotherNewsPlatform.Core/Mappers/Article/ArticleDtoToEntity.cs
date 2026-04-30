// using System.Runtime.InteropServices;
using AnotherNewsPlatform.Database.Entities;
using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.Core.Mappers;

[Mapper]
public partial class ArticleDtoToEntity
{
    [MapperIgnoreTarget(nameof(Article.Source))]
    [MapperIgnoreTarget(nameof(Article.Comments))]
    public partial Article ToEntity (ArticleDto dto);
}