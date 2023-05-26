using GeoLocationDemo.Core.Models;

namespace GeoLocationDemo.Services.IContractService
{
    public interface ILocationService
    {
        RouteDistanceModel GetLocations(int from, int to);
    }
}
