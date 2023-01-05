using News.Domain.Core;

namespace News.Domain.TopHeadlines
{
    public interface ITopHeadlineRepository : IRepository<TopHeadline, int>
    {
        Task<IEnumerable<TopHeadline?>> GetTopHeadlinesByAsync(string? countryCode, int? sourceId);
    }
}
