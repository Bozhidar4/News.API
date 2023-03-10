using News.API.Models;

namespace News.API.Services.Interfaces
{
    public interface ITopHeadlineService
    {
        Task<IEnumerable<TopHeadlineModel>> GetByCountryCodeAsync(string countryCode);
        Task<IEnumerable<TopHeadlineModel>> GetBySourceIdAsync(int sourceId);
    }
}
