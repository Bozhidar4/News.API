using Microsoft.AspNetCore.Mvc;
using News.API.Services.Interfaces;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService _sourceService;

        public SourcesController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sourceService.GetAsync());
        }
    }
}
