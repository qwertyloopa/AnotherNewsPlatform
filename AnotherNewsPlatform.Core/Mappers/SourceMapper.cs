using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.Core.Mappers;

[Mapper]
public partial class SourceMapper 
{
   public partial Source ToEntity(SourceDto sourceDto);
   public partial SourceDto ToDto(Source source);
}