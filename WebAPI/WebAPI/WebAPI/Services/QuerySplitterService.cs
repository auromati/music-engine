using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class QuerySplitterService : IQueryParserService
    {
        public string[] ParseQuery(string query)
        {
            return query.Split();
        }
    }
}
