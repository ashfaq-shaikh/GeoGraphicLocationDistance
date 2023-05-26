using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeoLocationDemo.Core.Entities
{
    public class Location : BaseEntity
    {
        public int ZIP { get; set; }
        public double LAT { get; set; }
        public double LNG { get; set; }
        public string City { get; set; }
    }
}
