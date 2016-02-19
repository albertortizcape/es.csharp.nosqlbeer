using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticsearchType(IdProperty = "id", Name = "Style")]
    public class Style
    {
        [Number]
        public int id { get; set; }

        [String(Index = FieldIndexOption.Analyzed)]
        public string name { get; set; }
    }
}
