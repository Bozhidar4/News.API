using News.Domain.Core;

namespace News.Domain.Countries
{
    public interface ICountryRepository : IRepository<Country, int>
    {
        Task<IList<string?>> GetAllCountryCodesAsync();
    }
}
