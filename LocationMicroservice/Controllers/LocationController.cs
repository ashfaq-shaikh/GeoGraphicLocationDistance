using GeoLocationDemo.Core.Entities;
using GeoLocationDemo.Core.Models;
using GeoLocationDemo.Services.IContractService;
using Microsoft.AspNetCore.Mvc;

namespace LocationMicroservice.Controllers
{
    [ApiController]
    [Route("api/location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet("{from}/{to}")]
        public IActionResult GetLocations(int from, int to)
        {
            try
            {
                var locations = _locationService.GetLocations(from, to);
                return Ok(new BaseResponse<RouteDistanceModel>(locations, true));
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLocations Error : " + ex.ToString());
                return BadRequest(new BaseResponse(false, ex.Message));
            }
        }
    }
}