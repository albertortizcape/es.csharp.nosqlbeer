using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticType(IdProperty = "id", Name = "Style")]
    public class Style
    {
        [ElasticProperty(Name = "_id", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int id { get; set; }

        [ElasticProperty(Name = "_name", Index = FieldIndexOption.Analyzed, Type = FieldType.String)]
        public string name { get; set; }
    }
}
