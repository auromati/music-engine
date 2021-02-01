using LemmaSharp.Classes;
using Microsoft.AspNetCore.Hosting;
using OpenNLP.Tools.NameFind;
using OpenNLP.Tools.Tokenize;
using SharpNL.WordNet;
using SharpNL.WordNet.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class QueryParserService : IQueryParserService
    {       
        private readonly string LEMMATIZER_DICT_PATH;
        private readonly string WORDNET_PATH;
        private readonly string NER_PATH;

        private readonly HashSet<string> JAMENDO_TAGS = new HashSet<string>
        {
            "sad", "calm", "energetic", "cheerful", "happy", "epic", "peaceful", "melodic", "weird", "relaxing",
            "soft", "spiritual", "dramatic", "dark", "nostalgic", "uplifting", "inspiring", "hopeful", "romantic",
            "oriental", "rock", "metal", "new wave", "jazz", "house", "pop", "dance", "electronic", "ambient",
            "minimal", "latin", "ebm", "idm", "techno", "r'n'b", "hip hop", "rap", "indie", "alternative",
            "blues", "country", "disco", "soul"
        };

        public QueryParserService(IHostingEnvironment hostingEnvironment)
        {
            LEMMATIZER_DICT_PATH = hostingEnvironment.ContentRootPath + "\\..\\WebAPI\\static\\full7z-multext-en.lem";
            WORDNET_PATH = hostingEnvironment.ContentRootPath + "\\..\\WebAPI\\static\\word-net-dict\\";
            NER_PATH = hostingEnvironment.ContentRootPath + "\\..\\WebAPI\\static\\name-find\\";
        }

        public QueryItems ParseQuery(string query)
        {
            IEnumerable<string> lemmas = LemmatizeQuery(query);
            List<string> tags = GetTags(lemmas);
            List<string> locations = GetLocations(query);

            return new QueryItems
            {
                Tags = tags,
                Locations = locations
            }; 
        }

        private List<string> GetLocations(string query)
        {
            var nameFinder = new EnglishNameFinder(NER_PATH);
            var models = new[] { "location" };
            var ner = nameFinder.GetNames(models, query);
            Regex locationRegex = new Regex("(?<=<location>)(.*?)(?=</location>)");
            var locationMatches = locationRegex.Matches(ner);

            return locationMatches.Select(x => x.Groups[0].Value).ToList();
        }

        private List<string> GetTags(IEnumerable<string> lemmas)
        {
            var tags = new List<string>();
            WordNet wn = new WordNet(new WordNetFileProvider(WORDNET_PATH));

            foreach (var lemma in lemmas)
            {
                if (JAMENDO_TAGS.Contains(lemma.ToLower()))
                {
                    tags.Add(lemma);
                    continue;
                }
                var synsets = wn.GetSynSets(lemma);
                foreach (var synset in synsets)
                {
                    foreach (var synonym in synset.Words)
                    {
                        if (JAMENDO_TAGS.Contains(synonym))
                        {
                            tags.Add(synonym);
                            break;
                        }
                    }
                }
            }
            return tags;

        }

        private IEnumerable<string> LemmatizeQuery(string query)
        {
            var tokenizer = new EnglishRuleBasedTokenizer(true);
            String[] tokens = tokenizer.Tokenize(query);
            var stream = File.OpenRead(LEMMATIZER_DICT_PATH);
            var lemmatizer = new Lemmatizer(stream);
            return tokens.Select(x => lemmatizer.Lemmatize(x));

        }
    }
}
