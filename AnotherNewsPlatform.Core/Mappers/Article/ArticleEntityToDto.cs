using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class ArticleEntityToDto
    {
        [MapperIgnoreSource(nameof(entity.Source))]
        [MapperIgnoreSource(nameof(entity.Comments))]
        public partial ArticleDto ToDto(Article entity);
    }
}
