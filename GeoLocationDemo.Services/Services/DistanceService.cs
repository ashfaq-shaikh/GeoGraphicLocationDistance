using GeoLocationDemo.Core.Models;
using GeoLocationDemo.Services.IContractService;
using Microsoft.Extensions.Options;

namespace GeoLocationDemo.Services.Services
{
    /// <summary>
    /// Distance Service is used to calculate the distance based on latitude and longiture.
    /// </summary>
    public class DistanceService : IDistanceService
    {
        private readonly MicroServiceSettings _microServiceSettings;
        public DistanceService(IOptions<MicroServiceSettings> microServiceSettings)
        {
            _microServiceSettings = microServiceSettings.Value;
        }

        private double ToRadians(double angleIn10thofaDegree)
        {
            // Angle in 10th
            // of a degree
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }

        /// <summary>
        /// This method will return the distance based on lat-long params.
        /// </summary>
        /// <param name="lat1">Pass latitude1</param>
        /// <param name="lat2">Pass latitude2</param>
        /// <param name="lon1">Pass longitude1</param>
        /// <param name="lon2">Pass longitude2</param>
        /// <returns></returns>
        private double GetDistance(double lat1, double lat2, double lon1, double lon2)
        {
            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            lon1 = ToRadians(lon1);
            lon2 = ToRadians(lon2);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            //double r = 6371;

            //if want correct result then need to be multiply is 4059

            double r = 3956;

            // calculate the result
            return (c * r);
        }

        /// <summary>
        /// This method will return locations and distace of zip codes.
        /// </summary>
        /// <param name="from">Pass from zipcode</param>
        /// <param name="to">Pass to zipcode</param>
        /// <returns></returns>
        public async Task<RouteDistanceModel> CalculateDistanceByZipCode(int from, int to)
        {
            var model = await GetLocationsByMicroService(from, to);

            if (model.From != null && model.To != null)
            {
                model.Distance = GetDistance(model.From.LAT, model.To.LAT, model.From.LNG, model.To.LNG);
            }

            return model;
        }

        /// <summary>
        /// This method will return location details from microservice.
        /// </summary>
        /// <param name="from">Pass from zipcode</param>
        /// <param name="to">Pass to zipcode</param>
        /// <returns></returns>
        public async Task<RouteDistanceModel> GetLocationsByMicroService(int from, int to)
        {
            RouteDistanceModel model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_microServiceSettings.LocationApiUrl);
                var response = await client.GetAsync(string.Format("location/{0}/{1}", from, to));

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var location = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResponse<RouteDistanceModel>>(result);
                    model = location.Data;
                }
            }

            return model;
        }
    }
}
