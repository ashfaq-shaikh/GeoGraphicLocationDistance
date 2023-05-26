using GeoLocationDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationDemo.Core.Models
{
    public class RouteDistanceModel
    {
        public Location From { get; set; }
        public Location To { get; set; }
        public double Distance { get; set; }
    }
}
