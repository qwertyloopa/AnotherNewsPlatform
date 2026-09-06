using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.SourceService;
public interface ISourceService
{
    public Task<SourceDto> GetSourceAsync(long sourceId);
    public Task CreateSourceAsync(SourceDto source);
    public Task DeleteSourceAsync(long sourceId);
    
    
}