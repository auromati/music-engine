using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace WebAPI.Services
{
    public class JamendoService : IJamendoService
    {
        private SparqlRemoteEndpoint _endpoint;

        private const string JAMENDO_URL = "http://dbtune.org/jamendo/sparql/";
        private const string GRAPH_NAME = "http://dbtune.org/jamendo/";

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

        public SparqlResultSet GetAlbumsByTag(IList<string> tags)
        {
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
                                                                        ORDER BY DESC(?tagCount)");
            return results;
        }
    }
}
