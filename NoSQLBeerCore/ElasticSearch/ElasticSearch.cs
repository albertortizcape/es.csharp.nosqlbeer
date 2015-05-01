using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Newtonsoft.Json;
using NoSQLBeerCore.Elements;

namespace NoSQLBeerCore.ElasticSearch
{
    public class ElasticSearch
    {
        public Uri UrlElasticSearchGraph { get; private set; }
        public ElasticsearchClient ElasticsearchGraphClient { get; private set; }

        private const string INDEX_NOSQLBEERS = "nosqlbeers";

        private const string TYPE_STYLE = "style";
        private const string TYPE_CATEGORY = "category";
        private const string TYPE_BREWERY = "brewery";
        private const string TYPE_BREWERY_GEOCODE = "brewery_geocode";
        private const string TYPE_BEER = "beer";

        public ElasticSearch(Uri pElasticSearchUri)
        {
            this.UrlElasticSearchGraph = pElasticSearchUri;
            var config = new ConnectionConfiguration(pElasticSearchUri);
            ElasticsearchClient client = new ElasticsearchClient(config);

            ElasticsearchGraphClient = client;
        }

        public void InsertBreweryGeocodes(int pBreweryGeocodeId, double pBrewevyLatitude, double pBreweryLongitude, string pBreweryAccuracy, int pBreweryId)
        {
            BreweryGeocode breweryGeocode = new BreweryGeocode() { id = pBreweryGeocodeId, latitude = pBrewevyLatitude, longitude = pBreweryLongitude };
            if (!string.IsNullOrEmpty(pBreweryAccuracy)) breweryGeocode.accuracy = pBreweryAccuracy;

            string breweryGeocodeJson = JsonConvert.SerializeObject(breweryGeocode);
            ElasticsearchGraphClient
                .Index(INDEX_NOSQLBEERS, TYPE_BREWERY_GEOCODE, pBreweryGeocodeId.ToString(), breweryGeocodeJson);
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

            string breweryJson = JsonConvert.SerializeObject(brewery);
            ElasticsearchGraphClient
                .Index(INDEX_NOSQLBEERS, TYPE_BREWERY, pBreweryId.ToString(), breweryJson);

        }

        public void InsertBeer(int pBeerId, string pBeerName, double pBeerAbv, string pBeerDescription, int pBreweryID, int pStyleID)
        {
            Beer beer = new Beer() { id = pBeerId, name = pBeerName, abv = pBeerAbv };
            if (!string.IsNullOrEmpty(pBeerDescription)) beer.description = pBeerDescription;

            string beerJson = JsonConvert.SerializeObject(beer);
            ElasticsearchGraphClient
                .Index(INDEX_NOSQLBEERS, TYPE_BEER, pBeerId.ToString(), beerJson);
        }

        public void InsertStyles(int pStyleID, string pStyleName)
        {
            Style style = new Style() { id = pStyleID, name = pStyleName };
            ElasticsearchGraphClient
                .Index(INDEX_NOSQLBEERS, TYPE_STYLE, pStyleID.ToString(), style);
        }
    }
}
