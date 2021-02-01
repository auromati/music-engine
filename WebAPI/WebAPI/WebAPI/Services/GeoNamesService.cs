using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Query;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class GeoNamesService : IGeoNamesService
    {
        private SparqlRemoteEndpoint _endpoint;

        public GeoNamesService()
        {
            this._endpoint = new SparqlRemoteEndpoint(new Uri("http://sparql.org/sparql"));
        }

        public Location GetLocation(string url)
        {
            var data = GetLocationName(url).FirstOrDefault();
            var name = "";
            var countryName = "";

            if (data == null)
                return new Location { CountryName = countryName, Name = name };

            name = data["name"].ToString();
            Console.WriteLine(name);
            var countryUrl = (data["country"] != null ? data["country"].ToString() : "");

            if (!string.IsNullOrWhiteSpace(countryUrl))
            {
                Console.WriteLine(countryUrl);
                var countryResult = GetLocationName(countryUrl).FirstOrDefault();
                if (countryResult != null)
                {
                    countryName = countryResult["name"].ToString();
                }
            }

            countryName = !string.IsNullOrWhiteSpace(countryName) ? countryName : name;

            return new Location { CountryName = countryName, Name = name };
        }

        private SparqlResultSet GetLocationName(string geoUrl)
        {
            if (!geoUrl.EndsWith("/"))
                geoUrl += "/";

            return this._endpoint.QueryWithResultSet("PREFIX gn: <http://www.geonames.org/ontology#>" +
                "select * from <" + geoUrl + "about.rdf>" +
                " where { ?a gn:name ?name . " +
                " OPTIONAL { ?a gn:parentCountry ?country .} }");
        }

    }
}
