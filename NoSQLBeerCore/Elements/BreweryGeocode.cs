using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticsearchType(IdProperty = "id", Name = "BreweryGeocode")]
    public class BreweryGeocode
    {
        [Number]
        public int id { get; set; }

        [Number]
        public double latitude { get; set; }

        [Number]
        public double longitude { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string accuracy { get; set; }

        [Number]
        public int brewery { get; set; }
    }
}
