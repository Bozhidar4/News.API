using Microsoft.EntityFrameworkCore;
using News.Domain.TopHeadlines;
using News.Persistence.Core;

namespace News.Persistence.Repositories
{
    public class TopHeadlineRepository :
        RepositoryBase<TopHeadline, NewsDBContext, int>,
        ITopHeadlineRepository
    {
        public TopHeadlineRepository(NewsDBContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<TopHeadline?>> GetTopHeadlinesByCountryCodeAsync(string countryCode)
        {
            return await Query
                .Where(q => q.CountryCode == countryCode)
                .ToListAsync();
        }
    }
}
