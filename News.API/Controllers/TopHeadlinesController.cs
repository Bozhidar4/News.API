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

        [HttpGet]
        [Route("{sourceId:int}")]
        public async Task<IActionResult> Get(int sourceId)
        {
            return Ok(await _topHeadlineService.GetBySourceIdAsync(sourceId));
        }
    }
}
