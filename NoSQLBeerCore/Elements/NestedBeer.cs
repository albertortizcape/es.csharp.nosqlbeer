using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    // Nest update to version 2.xx implies to update all types and properties
    // https://www.elastic.co/blog/ga-release-of-nest-2-0-our-dot-net-client-for-elasticsearch

    [ElasticsearchType(IdProperty = "id", Name = "NestedBeer")]
    public class NestedBeer
    {
        [Number]
        public int id { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string name { get; set; }

        [Number]
        public double abv { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string description { get; set; }

        [Nested(IncludeInParent=true)]
        public NestedStyle style { get; set; }

        public NestedBrewery brewery { get; set; }
    }

    [ElasticsearchType(IdProperty = "id", Name = "NestedStyle")]
    public class NestedStyle
    {
        [Number]
        public int id { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string name { get; set; }
    }

    [ElasticsearchType(IdProperty = "id", Name = "NestedBrewery")]
    public class NestedBrewery
    {
        [Number]
        public int id { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string name { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string address1 { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string address2 { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string city { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string state { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string code { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string country { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string phone { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string website { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string description { get; set; }

        [Nested(IncludeInParent = true)]
        public List<NestedBreweryGeocode> geocodes { get; set; }
    }

    [ElasticsearchType(IdProperty = "id", Name = "NestedBreweryGeocode")]
    public class NestedBreweryGeocode
    {
        [Number]
        public int id { get; set; }

        [Number]
        public double latitude { get; set; }

        [Number]
        public double longitude { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string accuracy { get; set; }
    }
}
