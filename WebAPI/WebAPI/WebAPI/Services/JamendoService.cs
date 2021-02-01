using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Parsing.Handlers;
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

        private readonly string STRING_SCHEMA = "http://www.w3.org/2001/XMLSchema#string";

        private IGeoNamesService _geoNamesService;

        public JamendoService(IGeoNamesService geoNamesService)
        {
            this._endpoint = new SparqlRemoteEndpoint(new Uri(JAMENDO_URL), GRAPH_NAME);
            this._geoNamesService = geoNamesService;
        }

        public IEnumerable<Album> GetAlbumsByTags(IEnumerable<string> tags, int page = 1, int pageSize = 10)
        {
            List<Album> results = new List<Album>();

            var resultsSet = this.QueryAlbumsByTags(tags, page, pageSize);
            foreach(var albumResult in resultsSet)
            {
                var album = GetAlbumByUrl(albumResult["alb"].ToString());
                album.Matches = album.Tags.Where(albumTag => tags.Any(tag => albumTag.IndexOf("/" + tag) == (albumTag.Length - tag.Length - 1))).Count();
                results.Add(album);
            }

            return results;
        }

        public Album GetAlbumByUrl(string albumUrl)
        {
            var query = @"SELECT ?p ?o ?makerName ?makerLocation
                        {
                          <" + albumUrl + @"> ?p ?o ;
                                              foaf:maker ?maker .
                          ?maker foaf:name ?makerName .
                          OPTIONAL { ?maker foaf:based_near ?makerLocation }  
                        }
                        ";
            var resultSet = _endpoint.QueryWithResultSet(query);

            return ParsePropertiesResultSetToAlbum(resultSet, albumUrl);
        }

        private Album ParsePropertiesResultSetToAlbum(SparqlResultSet resultSet, string albumUrl)
        {
            var title = GetSingleStringPropertyFromResultSet(resultSet, "title");
            var artist = GetSingleStringPropertyFromResultSet(resultSet, "maker", "p", "makerName");
            var tags = GetMultipleStringPropertiesFromResultSet(resultSet, "taggedWithTag");
            var imagePath = GetSingleStringPropertyFromResultSet(resultSet, "image");
            var date = GetSingleStringPropertyFromResultSet(resultSet, "date");
            DateTime.TryParse(date, out DateTime dateTimeParsed);
            var locationUrl = GetSingleStringPropertyFromResultSet(resultSet, "maker", "p", "makerLocation");
            Console.WriteLine(locationUrl);
            var location = this._geoNamesService.GetLocation(locationUrl);
            var country = location.CountryName;

            return new Album()
            {
                Title = title,
                Artist = artist,
                Tags = tags,
                ImagePath = imagePath,
                Url = albumUrl,
                ReleaseDate = dateTimeParsed,
                Location = location,
                Country = country,
            };
        }

        private string GetSingleStringPropertyFromResultSet(SparqlResultSet resultSet, string property)
        {
            return resultSet.Where(r => r["p"].ToString().Contains(property)).Select(r => r["o"].ToString().Replace($"^^{STRING_SCHEMA}", "")).FirstOrDefault() ?? string.Empty;
        }

        private string GetSingleStringPropertyFromResultSet(SparqlResultSet resultSet, string property, string pred, string obj)
        {
            return resultSet.Where(r => r[pred].ToString().Contains(property)).Select(r => r[obj].ToString().Replace($"^^{STRING_SCHEMA}", "")).FirstOrDefault() ?? string.Empty;
        }

        private IList<string> GetMultipleStringPropertiesFromResultSet(SparqlResultSet resultSet, string property)
        {
            return resultSet.Where(r => r["p"].ToString().Contains(property)).Select(r => r["o"].ToString().Replace($"^^{STRING_SCHEMA}", "")).ToList();
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
