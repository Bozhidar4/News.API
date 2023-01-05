using Microsoft.EntityFrameworkCore;
using News.Domain.TopHeadlines;
using News.Persistence.Core;
using System.Linq.Expressions;

namespace News.Persistence.Repositories
{
    public class TopHeadlineRepository :
        RepositoryBase<TopHeadline, NewsDBContext, int>,
        ITopHeadlineRepository
    {
        public TopHeadlineRepository(NewsDBContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<TopHeadline?>> GetTopHeadlinesByAsync(string? countryCode, int? sourceId)
        {
            Expression<Func<TopHeadline, bool>> condition = th =>
                (!string.IsNullOrWhiteSpace(countryCode) || th.CountryCode == countryCode) &&
                (!sourceId.HasValue || th.SourceId == sourceId.Value);

            return await Query
                .Where(condition)
                .ToListAsync();
        }
    }
}
