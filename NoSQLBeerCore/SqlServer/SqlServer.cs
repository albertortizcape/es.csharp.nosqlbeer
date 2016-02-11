using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSQLBeerCore.Elements;

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

        public static List<NestedBeer> GetSqlServerNestedBeers()
        {
            List<NestedBeer> nestedBeers = new List<NestedBeer>();

            // Get all data
            List<beers> beerList = GetSqlServerBeers();
            foreach (beers beer in beerList)
            {
                NestedBeer nestedBeer = new NestedBeer();
                nestedBeer.id = beer.ID;
                nestedBeer.name = beer.name;
                nestedBeer.abv = beer.abv;
                nestedBeer.description = beer.descript;

                FillStyles(nestedBeer, beer.style_id);
                FillBreweries(nestedBeer, beer.brewery_id);

                nestedBeers.Add(nestedBeer);

                Console.WriteLine(DateTime.Now + " BeerID " + beer.ID);
            }

            return nestedBeers;
        }

        private static void FillStyles(NestedBeer pNestedBeer, int pBeerStyleID)
        {
            List<styles> styleList = GetSqlServerStyles();
            var styles = styleList.Where(e => e.ID.Equals(pBeerStyleID));
            if (styles != null)
            {
                List<styles> beerStyles = styles.ToList<styles>();
                foreach (styles style in beerStyles)
                {
                    NestedStyle nestedStyle = new NestedStyle();
                    nestedStyle.id = style.ID;
                    nestedStyle.name = style.style_name;

                    pNestedBeer.style = nestedStyle;
                }
            }
        }

        private static void FillBreweries(NestedBeer pNestedBeer, int pBeerBreweryID)
        {
            List<breweries> breweryList = GetSqlServerBreweries();
            var breweries = breweryList.Where(e => e.ID.Equals(pBeerBreweryID));
            if (breweries != null)
            {
                List<breweries> beerBreweries = breweries.ToList<breweries>();
                foreach (breweries brewery in beerBreweries)
                {
                    NestedBrewery nestedBrewery = new NestedBrewery();
                    nestedBrewery.id = brewery.ID;
                    nestedBrewery.name = brewery.name;

                    FillBreweriesGeocodes(nestedBrewery, brewery.ID);

                    pNestedBeer.brewery = nestedBrewery;
                }
            } 
        }

        private static void FillBreweriesGeocodes(NestedBrewery pNestedBrewery, int pBreweryID)
        {
                List<breweries_geocode> breweryGeocodeList = GetSqlServerBreweries_Geocodes();
                var location = breweryGeocodeList.Where(e => e.brewery_id.Equals(pBreweryID));
                if (location != null)
                {
                    List<breweries_geocode> beerBreweries_Geocode = location.ToList<breweries_geocode>();
                    foreach (breweries_geocode brewery_geocode in beerBreweries_Geocode)
                    {
                        NestedBreweryGeocode nestedBreweryGeocode = new NestedBreweryGeocode();
                        nestedBreweryGeocode.id = brewery_geocode.ID;
                        nestedBreweryGeocode.latitude = brewery_geocode.latitude;
                        nestedBreweryGeocode.longitude = brewery_geocode.longitude;
                        nestedBreweryGeocode.accuracy = brewery_geocode.accuracy;

                        if (pNestedBrewery.geocodes == null)
                        {
                            pNestedBrewery.geocodes = new List<NestedBreweryGeocode>();
                        }
                        pNestedBrewery.geocodes.Add(nestedBreweryGeocode);
                    }
                }
        }
    }
}
