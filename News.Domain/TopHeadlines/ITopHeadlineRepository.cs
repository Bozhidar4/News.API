using News.Domain.Core;

namespace News.Domain.TopHeadlines
{
    public interface ITopHeadlineRepository : IRepository<TopHeadline, int>
    {
        Task<IEnumerable<TopHeadline?>> GetTopHeadlinesByCountryCodeAsync(string countryCode);
    }
}
