using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.API.Services.Interfaces;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopHeadlinesController : ControllerBase
    {
        private readonly ITopHeadlineService _topHeadlineService;

        public TopHeadlinesController(ITopHeadlineService topHeadlineService)
        {
            _topHeadlineService = topHeadlineService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string countryCode)
        {
            return Ok(await _topHeadlineService.GetByCountryCodeAsync(countryCode));
        }
    }
}
