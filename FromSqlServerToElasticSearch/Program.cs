using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSQLBeerCore.ElasticSearch;

namespace FromSqlServerToElasticSearch
{
    class Program
    {
        private static Uri pElasticSearchUri = new Uri("http://localhost:9200/");
        static void Main(string[] args)
        {
            ElasticSearch elasticSearch = new ElasticSearch(pElasticSearchUri);

            FromSqlServerToElasticSearch dataMigrator = new FromSqlServerToElasticSearch(elasticSearch);
            dataMigrator.Migrate();
        }
    }
}
