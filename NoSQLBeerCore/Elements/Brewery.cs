using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticsearchType(IdProperty = "id", Name = "Brewery")]
    public class Brewery
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
    }
}
