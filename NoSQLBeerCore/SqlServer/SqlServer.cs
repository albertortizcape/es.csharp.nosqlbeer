using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLBeerCore.SqlServer
{
    public class SqlServer
    {
        public static List<styles> GetSqlServerStyles()
        {
            List<styles> stylesList = new List<styles>();
            using (var beerDataBaseEntities = new OpenBeerDatabaseEntities())
            {
                stylesList = beerDataBaseEntities.styles.ToList();
            }

            return stylesList;
        }

        public static List<breweries> GetSqlServerBreweries()
        {
            List<breweries> breweriesList = new List<breweries>();
            using (var beerDataBaseEntities = new OpenBeerDatabaseEntities())
            {
                breweriesList = beerDataBaseEntities.breweries.ToList();
            }

            return breweriesList;
        }

        public static List<breweries_geocode> GetSqlServerBreweries_Geocodes()
        {
            List<breweries_geocode> breweries_geocodeList = new List<breweries_geocode>();
            using (var beerDataBaseEntities = new OpenBeerDatabaseEntities())
            {
                breweries_geocodeList = beerDataBaseEntities.breweries_geocode.ToList();
            }

            return breweries_geocodeList;
        }

        public static List<beers> GetSqlServerBeers()
        {
            List<beers> beers = new List<beers>();
            using (var beerDataBaseEntities = new OpenBeerDatabaseEntities())
            {
                beers = beerDataBaseEntities.beers.ToList();
            }

            return beers;
        }
    }
}
