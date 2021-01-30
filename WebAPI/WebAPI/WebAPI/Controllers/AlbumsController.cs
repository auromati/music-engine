using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            this._albumService = albumService;
        }

        // GET: api/Albums/5
        [HttpGet("{page}")]
        public IEnumerable<Album> Get(int page)
        {
            return _albumService.GetAllAlbums(page);
        }

        // GET: api/Albums/5
        [HttpGet("bytags/{tagsQuery}/{page}")]
        public IEnumerable<Album> GetByTagsQuery(string tagsQuery, int page)
        {
            return _albumService.GetAlbumsByTagsQuery(tagsQuery, page);
        }
    }
}
