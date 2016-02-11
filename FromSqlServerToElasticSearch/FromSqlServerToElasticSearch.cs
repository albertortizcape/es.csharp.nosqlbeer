using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSQLBeerCore;
using NoSQLBeerCore.ElasticSearch;
using NoSQLBeerCore.Elements;
using NoSQLBeerCore.SqlServer;

namespace FromSqlServerToElasticSearch
{
    public class FromSqlServerToElasticSearch : IMigratorBase
    {
        private ElasticSearch _ElasticSearchDB;
        public ElasticSearch ElasticSearchDB
        {
            get { return _ElasticSearchDB; }
        }

        public FromSqlServerToElasticSearch(ElasticSearch pElasticSearch)
        {
            this._ElasticSearchDB = pElasticSearch;
        }

        public void Migrate()
        {
            Console.WriteLine("ElasticSearch - First: Styles");
            MigrateStyles();

            Console.WriteLine("ElasticSearch - Second: Breweries");
            MigrateBreweries();

            Console.WriteLine("ElasticSearch - Third: Breweries geocodes");
            MigrateBreweriesGeocodes();

            Console.WriteLine("ElasticSearch - Fourth: Beers");
            MigrateBeers();

            Debugger.Break();

            Console.WriteLine("ElasticSearch - All DATA NESTED IN THE SAME OBJECT/DOCUMENT");
            MigrateAllNested();
        }

        public void MigrateBreweriesGeocodes()
        {
            List<breweries_geocode> listBreweries_Geocodes = SqlServer.GetSqlServerBreweries_Geocodes();
            foreach (breweries_geocode geocode in listBreweries_Geocodes)
            {
                Console.WriteLine(DateTime.Now + " BreweryGeocodeID " + geocode.ID);
                ElasticSearchDB.InsertBreweryGeocodes(geocode.ID, geocode.latitude, geocode.longitude, geocode.accuracy, geocode.brewery_id);
            }
        }

        public void MigrateBreweries()
        {
            List<breweries> listBreweries = SqlServer.GetSqlServerBreweries();
            foreach (breweries brewery in listBreweries)
            {
                Console.WriteLine(DateTime.Now + " BreweryID " + brewery.ID);
                ElasticSearchDB.InsertBrewery(brewery.ID, brewery.name, brewery.address1, brewery.address2, brewery.city, brewery.state, brewery.code, brewery.country, brewery.phone, brewery.website, brewery.descript);
            }
        }

        public void MigrateBeers()
        {
            List<beers> listBeers = SqlServer.GetSqlServerBeers();
            foreach (beers beer in listBeers)
            {
                Console.WriteLine(DateTime.Now + " BeerID " + beer.ID);
                ElasticSearchDB.InsertBeer(beer.ID, beer.name, beer.abv, beer.descript, beer.brewery_id, beer.style_id);
            }
        }

        public void MigrateStyles()
        {
            List<styles> listStyle = SqlServer.GetSqlServerStyles();
            foreach (styles style in listStyle)
            {
                Console.WriteLine(DateTime.Now + " StyleID " + style.ID);
                ElasticSearchDB.InsertStyles(style.ID, style.style_name);
            }
        }

        private void MigrateAllNested()
        {
            // https://www.devbridge.com/articles/getting-started-with-elastic-using-net-nest-library-part-two/
            // https://nest.azurewebsites.net/nest/indices/analyze.html

            /* Nested Data Structure
             * beer
             * - brewery
             * -- brewery_geocode
             * - style
             * */

            // Get nested data from SqlServer
            List<NestedBeer> beerList = SqlServer.GetSqlServerNestedBeers();

            // 1º Change the index
            ElasticSearchDB.INDEX_NOSQLBEERS = "nosqlbeersnested";

            foreach (NestedBeer nestedBeer in beerList)
            {
                // 2º Insert beer nested to elasticsearch
                ElasticSearchDB.InsertNestedBeer(nestedBeer);
            }


            // 3º Create a mapping

        }
    }
}
