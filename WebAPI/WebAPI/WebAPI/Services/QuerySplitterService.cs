using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class QuerySplitterService : IQueryParserService
    {
        public QueryItems ParseQuery(string query)
        {
            return new QueryItems
            {
                Tags = query.Split().ToList(),
                Locations = new List<string>()
            };
        }
    }
}
