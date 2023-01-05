using NewsAPI;

namespace News.API.Services.Interfaces
{
    public interface IHelperService
    {
        NewsApiClient GetNewsApiClient();
    }
}
