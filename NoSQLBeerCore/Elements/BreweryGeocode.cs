using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLBeerCore.Elements
{
    public class BreweryGeocode
    {
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string accuracy { get; set; }
    }
}
