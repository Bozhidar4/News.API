using News.API.Services.Interfaces;
using NewsAPI;
using System.ComponentModel.Design;

namespace News.API.Services
{
    public class HelperService : IHelperService
    {
        private readonly IConfiguration _configuration;

        public HelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NewsApiClient GetNewsApiClient()
        {
            return new NewsApiClient(_configuration.GetValue<string>("NewsAPI:Key"));
        }
    }
}
