using NewsAPI.Models;

namespace News.API.Services.Interfaces
{
    public interface IEverythingService
    {
        Task<ArticlesResult> GetEverythingByKeyword(string keyword);
    }
}
