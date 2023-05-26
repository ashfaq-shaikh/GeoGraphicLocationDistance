using GeoLocationDemo.Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationDemo.Services
{
    public abstract class MongoClientBase
    {
        protected readonly MongoDBSettings _mongoDBSettings;
        protected readonly MongoClient mongoClient;
        public MongoClientBase(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings.Value;

            var settings = MongoClientSettings.FromConnectionString(_mongoDBSettings.ConnectionURI);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            mongoClient = new MongoClient(settings);
        }
    }
}
