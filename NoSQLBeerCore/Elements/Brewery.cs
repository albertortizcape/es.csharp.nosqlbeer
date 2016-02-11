using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace NoSQLBeerCore.Elements
{
    [ElasticType(IdProperty = "id", Name = "Brewery")]
    public class Brewery
    {
        [ElasticProperty(Name = "_id", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.Long)]
        public int id { get; set; }

        [ElasticProperty(Name = "_name", Index = FieldIndexOption.Analyzed, Type = FieldType.String)]
        public string name { get; set; }

        [ElasticProperty(Name = "_address1", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string address1 { get; set; }

        [ElasticProperty(Name = "_address2", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string address2 { get; set; }

        [ElasticProperty(Name = "_city", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string city { get; set; }

        [ElasticProperty(Name = "_state", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string state { get; set; }

        [ElasticProperty(Name = "_code", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string code { get; set; }

        [ElasticProperty(Name = "_country", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string country { get; set; }

        [ElasticProperty(Name = "_phone", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string phone { get; set; }

        [ElasticProperty(Name = "_website", Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String)]
        public string website { get; set; }

        [ElasticProperty(Name = "_description", Index = FieldIndexOption.Analyzed, Type = FieldType.String)]
        public string description { get; set; }
    }
}
