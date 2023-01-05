using AutoMapper;
using News.API.Models;
using News.API.Services.Interfaces;
using News.Domain.TopHeadlines;

namespace News.API.Services
{
    public class TopHeadlineService : ITopHeadlineService
    {
        private readonly IMapper _mapper;
        private readonly ITopHeadlineRepository _topHeadlineRepository;

        public TopHeadlineService(
            IMapper mapper,
            ITopHeadlineRepository topHeadlineRepository)
        {
            _mapper = mapper;
            _topHeadlineRepository = topHeadlineRepository;
        }

        public async Task<IEnumerable<TopHeadlineModel>> GetByCountryCodeAsync(string countryCode)
        {
            return _mapper.Map<IEnumerable<TopHeadlineModel>>(
                await _topHeadlineRepository.GetTopHeadlinesByCountryCodeAsync(countryCode));
        }
    }
}
