using News.API.Models;

namespace News.API.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryModel>> GetAsync();
    }
}
