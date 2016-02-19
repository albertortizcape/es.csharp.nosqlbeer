using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using NoSQLBeerCore.Elements;

namespace NoSQLBeerCore.ElasticSearch
{
    public class ElasticSearch
    {
        // Nest and ElasticSearch.net nuget documentation:
        // https://github.com/elastic/elasticsearch-net

        public ElasticClient ElasticsearchNestClient { get; private set; }
        public ElasticLowLevelClient ElasticsearchLowLevelClient { get; private set; }

        public string INDEX_NOSQLBEERS { get; set; }

        private const string TYPE_STYLE = "style";
        private const string TYPE_CATEGORY = "category";
        private const string TYPE_BREWERY = "brewery";
        private const string TYPE_BREWERY_GEOCODE = "brewery_geocode";
        private const string TYPE_BEER = "beer";

        private const string TYPE_NESTED_BEER = "nestedbeer";

        public ElasticSearch(Uri pElasticSearchUri, string pIndex)
        {
            INDEX_NOSQLBEERS = pIndex;
            ElasticsearchNestClient = new ElasticClient(pElasticSearchUri);

            // We create a lowlevelclient for elastic search (is the old ElasticSearch.Net)
            ConnectionConfiguration config = new ConnectionConfiguration(pElasticSearchUri);
            ElasticsearchLowLevelClient = new ElasticLowLevelClient(config);
        }

        public void InsertBreweryGeocodes(int pBreweryGeocodeId, double pBrewevyLatitude, double pBreweryLongitude, string pBreweryAccuracy, int pBreweryId)
        {
            BreweryGeocode breweryGeocode = new BreweryGeocode() { id = pBreweryGeocodeId, latitude = pBrewevyLatitude, longitude = pBreweryLongitude, brewery = pBreweryId };
            if (!string.IsNullOrEmpty(pBreweryAccuracy)) breweryGeocode.accuracy = pBreweryAccuracy;

            ElasticsearchNestClient
                .Index(breweryGeocode, i => i.Index(INDEX_NOSQLBEERS).Type(TYPE_BREWERY_GEOCODE));
        }

        public void InsertBrewery(int pBreweryId, string pBreweryName, string pBreweryAddress1, string pBreweryAddress2, string pBreweryCity, string pBreweryState, string pBreweryCode, string pBreweryCountry, string pBreweryPhone, string pBreweryWebSite, string pBreweryDescription)
        {
            Brewery brewery = new Brewery() { id = pBreweryId, name = pBreweryName };
            if (!string.IsNullOrEmpty(pBreweryAddress1)) brewery.address1 = pBreweryAddress1;
            if (!string.IsNullOrEmpty(pBreweryAddress2)) brewery.address2 = pBreweryAddress2;
            if (!string.IsNullOrEmpty(pBreweryCity)) brewery.city = pBreweryCity;
            if (!string.IsNullOrEmpty(pBreweryState)) brewery.state = pBreweryState;
            if (!string.IsNullOrEmpty(pBreweryState)) brewery.code = pBreweryCode;
            if (!string.IsNullOrEmpty(pBreweryCountry)) brewery.country = pBreweryCountry;
            if (!string.IsNullOrEmpty(pBreweryPhone)) brewery.phone = pBreweryPhone;
            if (!string.IsNullOrEmpty(pBreweryWebSite)) brewery.website = pBreweryWebSite;
            if (!string.IsNullOrEmpty(pBreweryDescription)) brewery.description = pBreweryDescription;
            
            ElasticsearchNestClient
                .Index(brewery, i=>i.Index(INDEX_NOSQLBEERS).Type(TYPE_BREWERY));
        }

        public void InsertBeer(int pBeerId, string pBeerName, double pBeerAbv, string pBeerDescription, int pBreweryID, int pStyleID)
        {
            Beer beer = new Beer() { id = pBeerId, name = pBeerName, abv = pBeerAbv, style = pStyleID, brewery = pBreweryID };
            if (!string.IsNullOrEmpty(pBeerDescription)) beer.description = pBeerDescription;

            ElasticsearchNestClient
                .Index(beer, i => i.Index(INDEX_NOSQLBEERS).Type(TYPE_BEER));
        }

        public void InsertStyles(int pStyleID, string pStyleName)
        {
            Style style = new Style() { id = pStyleID, name = pStyleName };
            
            ElasticsearchNestClient
                .Index(style, i => i.Index(INDEX_NOSQLBEERS).Type(TYPE_STYLE));
        }

        public void InsertNestedBeer(NestedBeer pNestedBeer)
        {
            ElasticsearchNestClient
                .Index(pNestedBeer, i => i.Index(INDEX_NOSQLBEERS).Type(TYPE_NESTED_BEER));

            //string nestedBeerJson = JsonConvert.SerializeObject(pNestedBeer);
            //ElasticsearchLowLevelClient.Index<NestedBeer>(INDEX_NOSQLBEERS, TYPE_NESTED_BEER, nestedBeerJson);
        }
    }
}
