using AutoMapper;
using News.Domain.Countries;
using News.Domain.Sources;
using News.Domain.TopHeadlines;
using News.Shared.Persistence;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace News.API.Services
{
    public class NewsBackgroundService : BackgroundService
    {
        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITopHeadlineRepository _topHeadlineRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NewsBackgroundService(
            IConfiguration configuration,
            IMapper mapper,
            ITopHeadlineRepository topHeadlineRepository,
            ICountryRepository countryRepository,
            ISourceRepository sourceRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _topHeadlineRepository = topHeadlineRepository;
            _countryRepository = countryRepository;
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sources = await GetAllSourcesForCountriesInDbAsync();
            await InsertSourcesAsync(sources);

            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                var topHeadlines = await GetTopHeadlinesForCountriesInDbAsync();
                await InsertTopHeadlinesAsync(topHeadlines);
            }
        }

        private async Task<IEnumerable<TopHeadline>> GetTopHeadlinesForCountriesInDbAsync()
        {
            var topHeadlines = new List<TopHeadline>();
            var countries = await GetAllCountriesAsync();
            var sources = await GetAllSourcesAsync();

            foreach (var country in countries)
            {
                var topHeadlinesResponse = await GetTopHeadlinesAsync(country);

                if (topHeadlinesResponse.Status == Statuses.Ok)
                {
                    foreach (var topHeadline in topHeadlinesResponse.Articles)
                    {
                        var topHeadlineMapped = _mapper.Map<TopHeadline>(topHeadline);
                        topHeadlineMapped.CountryCode = country.Code;
                        topHeadlineMapped.SourceId = sources
                            .FirstOrDefault(s => s.NewsApiId == topHeadline.Source.Id && s.Name == topHeadline.Source.Name)?.Id;
                        topHeadlines.Add(topHeadlineMapped);
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

        // Getting only the sources for the 10 countries in the DB
        // Because we are using the developer version of the API and the results are limited to certain number
        // Source - https://github.com/News-API-gh/News-API-csharp/issues/4
        private async Task<IEnumerable<Domain.Sources.Source>> GetAllSourcesForCountriesInDbAsync()
        {
            
            var sources = new List<Domain.Sources.Source>();
            var countries = await GetAllCountriesAsync();

            foreach (var country in countries)
            {
                var results = await GetTopHeadlinesAsync(country);

                if (results.Status == Statuses.Ok)
                {
                    foreach (var article in results.Articles)
                    {
                        var sourceMapped = _mapper.Map<Domain.Sources.Source>(article.Source);

                        if (!sources.Any(s => s.Name == sourceMapped.Name && s.NewsApiId == sourceMapped.NewsApiId))
                        {
                            sources.Add(sourceMapped);
                        }
                    }
                }
            }

            return sources;
        }

        private async Task<ArticlesResult> GetTopHeadlinesAsync(Country? country)
        {
            var newsApiClient = new NewsApiClient(_configuration.GetValue<string>("NewsAPI:Key"));

            if (country is not null && country.Code is not null)
            {
                var results = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Country = (Countries)Enum.Parse(typeof(Countries), country.Code.ToUpperInvariant())
                });

                return results;
            }

            return new ArticlesResult { Status = Statuses.Error};
        }

        private async Task InsertSourcesAsync(IEnumerable<Domain.Sources.Source> sources)
        {
            var existingSources = await GetAllSourcesAsync();
            foreach (var source in sources)
            {
                if (!existingSources.Any(es => es.Name == source.Name && es.NewsApiId == source.NewsApiId))
                {
                    await _sourceRepository.AddAsync(source);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        private async Task<IEnumerable<Domain.Sources.Source>> GetAllSourcesAsync()
        {
            return await _sourceRepository.GetAllAsync();
        }
    }
}
