using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public IList<string> Tags { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Country { get; set; }

        public string Url { get; set; }

        public string ImagePath { get; set; }
    }
}
