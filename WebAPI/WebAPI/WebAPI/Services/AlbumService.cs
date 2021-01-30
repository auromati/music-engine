using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IJamendoService jamendoService;
        private readonly IQueryParserService parserService;

        private const int PAGE_SIZE = 10;

        public AlbumService(IJamendoService jamendoService,
                            IQueryParserService parserService)
        {
            this.jamendoService = jamendoService;
            this.parserService = parserService;
        }

        public IEnumerable<Album> GetAlbumsBySearchQuery(string searchQuery, int page = 1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAllAlbums(int page = 1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAlbumsByTagsQuery(string tagsQuery, int page=1)
        {
            var tags = parserService.ParseQuery(tagsQuery);

            return jamendoService.GetAlbumsByTags(tags, page, PAGE_SIZE);
        }
    }
}
