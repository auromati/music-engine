﻿using System.Collections.Generic;
using WebAPI.Models; 

namespace WebAPI.Services
{
    public interface IJamendoService
    {
        IEnumerable<Album> GetAlbumsByTags(IEnumerable<string> tags, int page = 1, int pageSize = 10);
    }
}