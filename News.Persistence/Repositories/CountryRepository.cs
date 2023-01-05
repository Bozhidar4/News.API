using Microsoft.EntityFrameworkCore;
using News.Domain.Countries;
using News.Persistence.Core;

namespace News.Persistence.Repositories
{
    public class CountryRepository :
        RepositoryBase<Country, NewsDBContext, int>,
        ICountryRepository
    {
        public CountryRepository(NewsDBContext dbContext)
            : base(dbContext)
        { }

        public async Task<IList<string?>> GetAllCountryCodesAsync()
        {
            return await Query
                .Select(c => c.Code)
                .ToListAsync();
        }
    }
}
