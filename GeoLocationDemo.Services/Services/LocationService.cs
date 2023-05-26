using GeoLocationDemo.Core.Entities;
using GeoLocationDemo.Core.Models;
using GeoLocationDemo.Services.IContractService;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace GeoLocationDemo.Services.Services
{
    public class LocationService : MongoClientBase, ILocationService
    {
        public LocationService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
        }

        public RouteDistanceModel GetLocations(int from, int to)
        {
            //return GetLocationFromFile(from, to);
            return GetLocationFromMongo(from, to);
        }


        /// <summary>
        /// using this method we will get the data from local file for the zip codes.
        /// </summary>
        /// <param name="from">Pass from zipcode</param>
        /// <param name="to">Pass to zipcode</param>
        /// <returns></returns>
        private RouteDistanceModel GetLocationFromFile(int from, int to)
        {
            RouteDistanceModel model = new RouteDistanceModel();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "utils/GeoLocation.json");
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(json);
                model.From = locations.Where(x=> x.ZIP == from).FirstOrDefault();
                model.To = locations.Where(x=> x.ZIP == to).FirstOrDefault();
            }

            return model;
        }

        /// <summary>
        /// This method will get the data from MongoDB database for the zip codes. 
        /// </summary>
        /// <param name="from">Pass from zipcode</param>
        /// <param name="to">Pass to zipcode</param>
        /// <returns></returns>
        private RouteDistanceModel GetLocationFromMongo(int from, int to)
        {
            RouteDistanceModel model = new RouteDistanceModel();
            
            IMongoDatabase database = mongoClient.GetDatabase(_mongoDBSettings.DatabaseName);
            var locations = database.GetCollection<Location>(_mongoDBSettings.CollectionName);
            model.From = locations.Find(x => x.ZIP == from).FirstOrDefault();
            model.To = locations.Find(x => x.ZIP == to).FirstOrDefault();
            return model;
        }
    }
}
