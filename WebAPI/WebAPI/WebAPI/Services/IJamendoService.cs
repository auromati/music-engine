using System.Collections.Generic;
using WebAPI.Models; 

namespace WebAPI.Services
{
    public interface IJamendoService
    {
        Album GetAlbumByUrl(string albumUrl);
        IEnumerable<Album> GetAlbumsByTags(IEnumerable<string> tags, int page = 1, int pageSize = 10);
    }
}