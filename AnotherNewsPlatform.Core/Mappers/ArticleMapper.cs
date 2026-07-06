using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class ArticleMapper
    {
        [MapperIgnoreSource(nameof(Article.Source))]
        public partial ArticleDto ToDto(Article articleEntity);

        [MapperIgnoreTarget(nameof(Article.Source))]
        [MapProperty(nameof(ArticleDto.SourceId), nameof(Article.SourceId))]
        public partial Article ToEntity(ArticleDto articleDto);

        
    }
}
