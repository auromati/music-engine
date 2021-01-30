using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class JamendoService : IJamendoService
    {
        private SparqlRemoteEndpoint _endpoint;

        private const string JAMENDO_URL = "http://dbtune.org/jamendo/sparql/";
        private const string GRAPH_NAME = "http://dbtune.org/jamendo/";

        private const string TAG_PREFIX = "<http://dbtune.org/jamendo/tag/";

        private readonly string PREFIXES = @"PREFIX geo: <http://www.geonames.org/ontology#>
                                             PREFIX wgs: <http://www.w3.org/2003/01/geo/wgs84_pos#>
                                             PREFIX mo: <http://purl.org/ontology/mo/>
                                             PREFIX foaf: <http://xmlns.com/foaf/0.1/>
                                             PREFIX tags: <http://www.holygoat.co.uk/owl/redwood/0.1/tags/>
                                             PREFIX dc: <http://purl.org/dc/elements/1.1/>
                                             PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>";

        public JamendoService()
        {
            this._endpoint = new SparqlRemoteEndpoint(new Uri(JAMENDO_URL), GRAPH_NAME);
        }

        public IEnumerable<Album> GetAlbumsByTags(IEnumerable<string> tags, int page = 1, int pageSize = 10)
        {
            List<Album> results = new List<Album>();

            var resultsSet = this.QueryAlbumsByTags(tags, page, pageSize);
            foreach(var albumResult in resultsSet)
            {
                results.Add(new Album
                {
                    Url = albumResult["alb"].ToString()
                });
            }

            return results;
        }
        public SparqlResultSet GetAlbumByUrl(string albumUrl)
        {
            var query = $"DESCRIBE <{albumUrl}>";

            //Get the result
            var g = _endpoint.QueryWithResultSet(query);

            return g;
        }

        private SparqlResultSet QueryAlbumsByTags(IEnumerable<string> tags, int page=1, int pageSize=10)
        {
            tags = tags.Select(t => t.StartsWith(TAG_PREFIX) ? t : TAG_PREFIX + t + ">");

            SparqlResultSet results = this._endpoint.QueryWithResultSet(PREFIXES + @"
                                                                        SELECT ?an ?tag ?albName ?tagCount ?alb
                                                                        WHERE
                                                                        {
                                                                            SELECT ?an ?tag ?albName (COUNT(?tag) as ?tagCount) ?alb
                                                                            WHERE
                                                                            { ?a 
                                                                                a mo:MusicArtist; 
                                                                                foaf:name ?an;
                                                                                foaf:made ?alb.
                                                                            ?alb tags:taggedWithTag ?tag ;
                                                                                    dc:title ?albName .
                                                                                FILTER (?tag IN (" + string.Join(',', tags) + @"))
                                                                            }
                                                                            GROUP BY ?albName ?an ?alb
                                                                        }
                                                                        ORDER BY DESC(?tagCount)" + BuildLimitQuery(page, pageSize));
            return results;
        }

        private string BuildLimitQuery(int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;

            return $"LIMIT {pageSize} OFFSET {offset}";
        }
    }
}
