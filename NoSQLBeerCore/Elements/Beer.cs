using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticsearchType(IdProperty = "id", Name = "Beer")]
    public class Beer
    {
        [Number]
        public int id { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string name { get; set; }

        [Number]
        public double abv { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string description { get; set; }

        [Number]
        public int style { get; set; }

        [Number]
        public int brewery { get; set; }
    }
}
