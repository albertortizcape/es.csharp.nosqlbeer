using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSQLBeerCore;
using NoSQLBeerCore.Neo4j;
using NoSQLBeerCore.SqlServer;

namespace FromSqlServerToNeo4j
{
    public class FromSqlServerToNeo4j : IMigratorBase
    {
        private Neo4j _Neo4jDB;
        public Neo4j Neo4jDB
        {
            get { return _Neo4jDB; }
        }

        public FromSqlServerToNeo4j(Neo4j pNeo4jDB)
        {
            this._Neo4jDB = pNeo4jDB;
        }

        public void Migrate()
        {
            Console.WriteLine("Neo4j - First: Styles");
            MigrateStyles();

            Console.WriteLine("Neo4j - Second: Breweries");
            MigrateBreweries();

            Console.WriteLine("Neo4j - Third: Breweries geocodes");
            MigrateBreweriesGeocodes();

            Console.WriteLine("Neo4j - Fourth: Beers");
            MigrateBeers();
        }

        public void MigrateBreweriesGeocodes()
        {
            List<breweries_geocode> listBreweries_Geocodes = SqlServer.GetSqlServerBreweries_Geocodes();
            foreach (breweries_geocode geocode in listBreweries_Geocodes)
            {
                Console.WriteLine(DateTime.Now + " BreweryGeocodeID " + geocode.ID);
                Neo4jDB.InsertBreweryGeocode(geocode.ID, geocode.latitude, geocode.longitude, geocode.accuracy, geocode.brewery_id);
            }
        }

        public void MigrateBreweries()
        {
            List<breweries> listBreweries = SqlServer.GetSqlServerBreweries();
            foreach (breweries brewery in listBreweries)
            {
                Console.WriteLine(DateTime.Now + " BreweryID " + brewery.ID);
                Neo4jDB.InsertBrewery(brewery.ID, brewery.name, brewery.address1, brewery.address2, brewery.city, brewery.state, brewery.code, brewery.country, brewery.phone, brewery.website, brewery.descript);
            }
        }

        public void MigrateBeers()
        {
            List<beers> listBeers = SqlServer.GetSqlServerBeers();
            foreach (beers beer in listBeers)
            {
                Console.WriteLine(DateTime.Now + " BeerID " + beer.ID);
                Neo4jDB.InsertBeer(beer.ID, beer.name, beer.abv, beer.srm, beer.upc, beer.descript, beer.brewery_id, beer.style_id);
            }
        }

        public void MigrateStyles()
        {
            List<styles> listStyle = SqlServer.GetSqlServerStyles();
            foreach (styles style in listStyle)
            {
                Console.WriteLine(DateTime.Now + " StyleID " + style.ID);
                Neo4jDB.InsertStyles(style.ID, style.style_name);
            }
        }
    }
}
