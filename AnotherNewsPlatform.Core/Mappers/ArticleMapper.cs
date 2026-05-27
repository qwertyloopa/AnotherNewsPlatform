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
        [MapperIgnoreSource(nameof(Article.Source))]
        public partial ArticleDto ToDto(Article articleEntity);

        [MapperIgnoreTarget(nameof(Article.Source))]
        public partial Article ToEntity(ArticleDto articleDto);
    }
}
