using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.SourceService;
public interface ISourceService
{
    public Task GetSourceAsync();
    public Task CreateSourceAsync(SourceDto source);
}