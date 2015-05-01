using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using NoSQLBeerCore.Elements;

namespace NoSQLBeerCore.Neo4j
{
    public class Neo4j
    {
        public Uri UrlNeo4jGraph { get; private set; }
        public static IGraphClient GraphClient { get; private set; }

        private const string CYPHER_STYLE_LABEL = "style";
        private const string CYPHER_BEER_LABEL = "beer";
        private const string CYPHER_BREWERY_LABEL = "brewery";
        private const string CYPHER_BREWERYGEOCODE_LABEL = "brewerygeocode";

        private const string CYPHER_STYLE_NODE = "Style";
        private const string CYPHER_BEER_NODE = "Beer";
        private const string CYPHER_BREWERY_NODE = "Brewery";
        private const string CYPHER_BREWERYGEOCODE_NODE = "BreweryGeocode";

        private const string CYPHER_CREATE_RELATIONSHIP = "CREATED";
        private const string CYPHER_STYLIZED_RELATIONSHIP = "STYLIZED";
        private const string CYPHER_HASFACTORY_RELATIONSHIP = "HASFACTORY";

        public Neo4j(Uri pNeo4jUri)
        {
            this.UrlNeo4jGraph = pNeo4jUri;
            var client = new GraphClient(pNeo4jUri);
            client.Connect();

            GraphClient = client;
        }

        // http://stackoverflow.com/questions/19534511/how-to-create-a-node-with-neo4jclient-in-neo4j-v2
        // https://github.com/Readify/Neo4jClient/wiki/cypher-examples
        //GraphClient.Create(new cat() {ID = pCategoryID, Name = pCategoryName});

        public void InsertBeer(int pBeerId, string pBeerName, double pBeerAbv, double pBeerSrm, int pBeerUpc, string pBeerDescription, int pBreweryID, int pStyleID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(" + CYPHER_BEER_LABEL + ":" + CYPHER_BEER_NODE + " {");
            sb.Append("id:" + pBeerId + ", ");
            sb.Append("name:'" + Utils.CleanString(pBeerName) + "', ");
            sb.Append("abv: '" + pBeerAbv + "', ");
            sb.Append("srm: '" + pBeerSrm + "', ");
            sb.Append("upc: '" + pBeerUpc + "'");
            if (!string.IsNullOrEmpty(pBeerDescription)) sb.Append(", description:'" + Utils.CleanString(pBeerDescription) + "'");
            sb.Append("})");

            GraphClient.Cypher
                .Create(sb.ToString())
                .ExecuteWithoutResults();

            GraphClient.Cypher
                .Create("INDEX ON :" + CYPHER_BEER_NODE + "(id)")
                .ExecuteWithoutResults();

            if (pBreweryID != -1)
            {
                GraphClient.Cypher
                    .Match("(" + CYPHER_BREWERY_LABEL + ":" + CYPHER_BREWERY_NODE + ")", "(" + CYPHER_BEER_LABEL + ":" + CYPHER_BEER_NODE + ")")
                    .Where((Brewery brewery) => brewery.id == pBreweryID)
                    .AndWhere((Beer beer) => beer.id == pBeerId)
                    .Create(CYPHER_BREWERY_LABEL + "-[:" + CYPHER_CREATE_RELATIONSHIP + "]->" + CYPHER_BEER_LABEL)
                    .ExecuteWithoutResults();
            }

            if (pStyleID != -1)
            {
                GraphClient.Cypher
                    .Match("(" + CYPHER_STYLE_LABEL + ":" + CYPHER_STYLE_NODE + ")", "(" + CYPHER_BEER_LABEL + ":" + CYPHER_BEER_NODE + ")")
                    .Where((Style style) => style.id == pStyleID)
                    .AndWhere((Beer beer) => beer.id == pBeerId)
                    .Create(CYPHER_STYLE_LABEL + "-[:" + CYPHER_STYLIZED_RELATIONSHIP + "]->" + CYPHER_BEER_LABEL)
                    .ExecuteWithoutResults();
            }
        }

        public void InsertBrewery(int pBreweryId, string pBreweryName, string pBreweryAddress1, string pBreweryAddress2, string pBreweryCity, string pBreweryState, string pBreweryCode, string pBreweryCountry, string pBreweryPhone, string pBreweryWebSite, string pBreweryDescription)
        {
            string query = "";
            query += "(" + CYPHER_BREWERY_LABEL + ":" + CYPHER_BREWERY_NODE + " {";
            query += "id:" + pBreweryId + ", ";
            if (!string.IsNullOrEmpty(pBreweryName)) query += "name:'" + Utils.CleanString(pBreweryName) + "', ";
            if (!string.IsNullOrEmpty(pBreweryAddress1)) query += "address1:'" + Utils.CleanString(pBreweryAddress1) + "', ";
            if (!string.IsNullOrEmpty(pBreweryAddress2)) query += "address2:'" + Utils.CleanString(pBreweryAddress2) + "', ";
            if (!string.IsNullOrEmpty(pBreweryCity)) query += "city:'" + Utils.CleanString(pBreweryCity) + "', ";
            if (!string.IsNullOrEmpty(pBreweryState)) query += "state:'" + Utils.CleanString(pBreweryState) + "', ";
            if (!string.IsNullOrEmpty(pBreweryCode)) query += "code:'" + Utils.CleanString(pBreweryCode) + "', ";
            if (!string.IsNullOrEmpty(pBreweryCountry)) query += "country:'" + Utils.CleanString(pBreweryCountry) + "', ";
            if (!string.IsNullOrEmpty(pBreweryPhone)) query += "phone:'" + Utils.CleanString(pBreweryPhone) + "', ";
            if (!string.IsNullOrEmpty(pBreweryWebSite)) query += "website:'" + Utils.CleanString(pBreweryWebSite) + "', ";
            if (!string.IsNullOrEmpty(pBreweryDescription)) query += "description:'" + Utils.CleanString(pBreweryDescription) + "', ";

            if (query.EndsWith(", "))
            {
                query = query.Substring(0, query.Length - 2);
            }

            query += "})";

            GraphClient.Cypher
                .Create(query)
                .ExecuteWithoutResults();

            GraphClient.Cypher
                .Create("INDEX ON :" + CYPHER_BREWERY_NODE + "(id)")
                .ExecuteWithoutResults();
        }

        public void InsertBreweryGeocode(int pBreweryGeocodeID, double pBrewevyLatitude, double pBreweryLongitude, string pBreweryAccuracy, int pBreweryID)
        {
            string query = "";
            query += "(" + CYPHER_BREWERYGEOCODE_LABEL + ":" + CYPHER_BREWERYGEOCODE_NODE + " {"; ;
            query += "id: " + pBreweryGeocodeID + ", ";
            query += "latitude:'" + pBrewevyLatitude + "', ";
            query += "longitude:'" + pBreweryLongitude + "', ";
            query += "accuracy:'" + pBreweryAccuracy + "'}) ";

            GraphClient.Cypher
                .Create(query)
                .ExecuteWithoutResults();

            GraphClient.Cypher
                .Create("INDEX ON :" + CYPHER_BREWERYGEOCODE_NODE + "(id)")
                .ExecuteWithoutResults();

            GraphClient.Cypher
                    .Match("(" + CYPHER_BREWERY_LABEL + ":" + CYPHER_BREWERY_NODE + ")", "(" + CYPHER_BREWERYGEOCODE_LABEL + ":" + CYPHER_BREWERYGEOCODE_NODE + ")")
                    .Where((Brewery brewery) => brewery.id == pBreweryID)
                    .AndWhere((BreweryGeocode brewerygeocode) => brewerygeocode.id == pBreweryGeocodeID)
                    .Create(CYPHER_BREWERY_LABEL + "-[:" + CYPHER_HASFACTORY_RELATIONSHIP + "]->" + CYPHER_BREWERYGEOCODE_LABEL)
                    .ExecuteWithoutResults();

        }

        public void InsertStyles(int pStyleID, string pStyleName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(" + CYPHER_STYLE_LABEL + ":" + CYPHER_STYLE_NODE + " {");
            sb.Append("id:" + pStyleID + ", ");
            sb.Append("name:'" + pStyleName + "'");
            sb.Append("})");

            GraphClient.Cypher
                .Create(sb.ToString())
                .ExecuteWithoutResults();

            GraphClient.Cypher
                .Create("INDEX ON :" + CYPHER_STYLE_NODE + "(id)")
                .ExecuteWithoutResults();
        }
    }
}

