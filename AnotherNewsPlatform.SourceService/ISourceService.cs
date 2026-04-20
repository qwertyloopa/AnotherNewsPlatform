using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.SourceService;
public interface ISourceService
{
    public Task GetSourceAsync();
    public Task CreateSourceAsync(SourceDto source);
}