using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.WebApi.Models;

namespace AnotherNewsPlatform.WebApi.Mappers;

[Mapper]
public partial class ArticleMapper
{
    public partial АrticleModel FromDtoToModel(ArticleDto dto);
    public partial ArticleDto FromModelToDto(АrticleModel model);
}