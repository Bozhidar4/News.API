using News.Domain.Sources;
using News.Persistence.Core;

namespace News.Persistence.Repositories
{
    public class SourceRepository :
        RepositoryBase<Source, NewsDBContext, int>,
        ISourceRepository
    {
        public SourceRepository(NewsDBContext dbContext)
            : base(dbContext)
        { }
    }
}
