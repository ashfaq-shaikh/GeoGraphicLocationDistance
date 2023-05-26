using GeoLocationDemo.Core.Models;

namespace GeoLocationDemo.Services.IContractService
{
    public interface IDistanceService
    {
        Task<RouteDistanceModel> CalculateDistanceByZipCode(int from, int to);
    }
}
