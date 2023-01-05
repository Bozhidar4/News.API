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
    }
}
