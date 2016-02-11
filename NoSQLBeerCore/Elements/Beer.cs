using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticType(IdProperty = "id", Name = "Beer")]
    public class Beer
    {
        [ElasticProperty(Name = "_id", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int id { get; set; }

        [ElasticProperty(Name = "_name", Index = FieldIndexOption.Analyzed, Type = FieldType.String)]
        public string name { get; set; }

        [ElasticProperty(Name = "_abv", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Double)]
        public double abv { get; set; }

        [ElasticProperty(Name = "_description", Index = FieldIndexOption.Analyzed, Type = FieldType.String)]
        public string description { get; set; }

        [ElasticProperty(Name = "_style", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int style { get; set; }

        [ElasticProperty(Name = "_brewery", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int brewery { get; set; }
    }
}
