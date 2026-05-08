using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class ArticleMapper
    {
        [MapperIgnoreSource(nameof(articleEntity.Source))]
        [MapperIgnoreSource(nameof(articleEntity.Comments))]
        public partial ArticleDto ToDto(Article articleEntity);

        [MapperIgnoreTarget(nameof(Article.Source))]
        [MapperIgnoreTarget(nameof(Article.Comments))]
        public partial Article ToEntity(ArticleDto articleDto);
    }
}
