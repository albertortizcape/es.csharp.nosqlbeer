using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticType(IdProperty = "id", Name = "BreweryGeocode")]
    public class BreweryGeocode
    {
        [ElasticProperty(Name = "_id", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int id { get; set; }

        [ElasticProperty(Name = "_latitude", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Double)]
        public double latitude { get; set; }

        [ElasticProperty(Name = "_longitude", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Double)]
        public double longitude { get; set; }

        [ElasticProperty(Name = "_accuracy", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string accuracy { get; set; }

        [ElasticProperty(Name = "_brewery", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int brewery { get; set; }
    }
}
