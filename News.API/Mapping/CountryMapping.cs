using AutoMapper;
using News.API.Models;
using News.Domain.Countries;

namespace News.API.Mapping
{
    public class CountryMapping : Profile
    {
        public CountryMapping()
        {
            CreateMap<Country, CountryModel>();
        }
    }
}
