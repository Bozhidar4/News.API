using Microsoft.AspNetCore.Mvc;
using News.API.Services.Interfaces;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EverythingController : ControllerBase
    {
        private readonly IEverythingService _everythingService;

        public EverythingController(IEverythingService everythingService)
        {
            _everythingService = everythingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string keyword)
        {
            return Ok(await _everythingService.GetEverythingByKeyword(keyword));
        }
    }
}
