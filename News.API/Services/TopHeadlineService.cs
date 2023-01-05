using AutoMapper;
using News.API.Services.Interfaces;
using News.Domain.Countries;
using News.Domain.TopHeadlines;
using News.Shared.Persistence;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace News.API.Services
{
    public class TopHeadlineService : BackgroundService, ITopHeadlineService
    {
        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITopHeadlineRepository _topHeadlineRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TopHeadlineService(
            IConfiguration configuration,
            IMapper mapper,
            ITopHeadlineRepository topHeadlineRepository,
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _topHeadlineRepository = topHeadlineRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                var topHeadlines = await GetTopHeadlinesForCoutriesAsync();
                await InsertTopHeadlinesAsync(topHeadlines);
            }
        }

        private async Task<IEnumerable<TopHeadline>> GetTopHeadlinesForCoutriesAsync()
        {
            var topHeadlines = new List<TopHeadline>();
            var newsApiClient = new NewsApiClient(_configuration.GetValue<string>("NewsAPI:Key"));
            var countries = await _countryRepository.GetAllAsync();

            foreach (var country in countries)
            {
                if (country is not null && country.Code is not null)
                {
                    var topHeadlinesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                    {
                        Country = (Countries)Enum.Parse(typeof(Countries), country.Code.ToUpperInvariant())
                    });

                    if (topHeadlinesResponse.Status == Statuses.Ok)
                    {
                        foreach (var topHeadline in topHeadlinesResponse.Articles)
                        {
                            var topHeadlineMapped = _mapper.Map<TopHeadline>(topHeadline);
                            topHeadlineMapped.CountryCode = country.Code;
                            topHeadlines.Add(topHeadlineMapped);
                        }
                    }
                }
            }
            
            return topHeadlines;
        }

        private async Task InsertTopHeadlinesAsync(IEnumerable<TopHeadline> topHeadlines)
        {
            foreach (var topHeadline in topHeadlines)
            {
                await _topHeadlineRepository.AddAsync(topHeadline);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
