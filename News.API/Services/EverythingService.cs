using AutoMapper;
using News.API.Services.Interfaces;
using NewsAPI.Models;

namespace News.API.Services
{
    public class EverythingService : IEverythingService
    {
        private readonly IHelperService _helperService;

        public EverythingService(
            IHelperService helperService)
        {
            _helperService = helperService;
        }

        public async Task<ArticlesResult> GetEverythingByKeyword(string keyword)
        {
            var newsApiClient = _helperService.GetNewsApiClient();
            var results = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = keyword
            });

            return results;
        }
    }
}
