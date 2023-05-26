using GeoLocationDemo.Core.Models;
using GeoLocationDemo.Services.IContractService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocationDemo.Controllers
{
    [ApiController]
    [Route("api/distance")]
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceService _distanceService;
        private readonly ILogger<DistanceController> _logger;

        public DistanceController(ILogger<DistanceController> logger, IDistanceService distanceService)
        {
            _logger = logger;
            _distanceService = distanceService;
        }

        [HttpGet("{from}/{to}")]
        public async Task<IActionResult> GetDistance(int from, int to)
        {
            try
            {
                var distance = await _distanceService.CalculateDistanceByZipCode(from, to);
                return Ok(new BaseResponse<RouteDistanceModel>(distance, true));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error GetDistance : " + ex.ToString());
                return BadRequest(new BaseResponse(false, ex.Message));
            }
        }
    }
}
