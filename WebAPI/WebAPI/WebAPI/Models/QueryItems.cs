using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class QueryItems
    {
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> Locations { get; set; } = new List<string>();
    }
}
