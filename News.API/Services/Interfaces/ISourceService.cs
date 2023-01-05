using News.API.Models;

namespace News.API.Services.Interfaces
{
    public interface ISourceService
    {
        Task<IEnumerable<SourceModel>> GetAsync();
    }
}
