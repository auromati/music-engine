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
        [HttpGet("{page}", Name = "Get")]
        public IEnumerable<Album> Get(string searchQuery, int page)
        {
            return _albumService.GetAlbumsBySearchQuery(searchQuery, page);
        }
    }
}
