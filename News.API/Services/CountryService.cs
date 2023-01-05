using AutoMapper;
using News.API.Models;
using News.API.Services.Interfaces;
using News.Domain.Countries;

namespace News.API.Services
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryService(
            IMapper mapper, 
            ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryModel>> GetAsync()
        {
            return _mapper.Map<IEnumerable<CountryModel>>(await _countryRepository.GetAllAsync());
        }
    }
}
