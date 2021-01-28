using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class MockAlbumService : IAlbumService
    {
        private const int PAGE_SIZE = 10;

        private List<Album> _albumsRepository;

        public MockAlbumService()
        {
            _albumsRepository = new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Artist = "Kaczor CHGW",
                    Title = "Nie jest kolorowo",
                    Country = "Polska",
                    Tags = new []{"Prawilny rap", "hip-hop", "pop", "gangsta rap", "skrrr", "sztywniutko"}.ToList(),
                    ImagePath = @"https://cdnb.artstation.com/p/assets/images/images/017/452/613/large/nevisa-nani-g-ba-album-front-coverart-watermarked.jpg?1556047027",
                    ReleaseDate = DateTime.Now.AddYears(-1),
                    Url = "www.policja.pl"
                },
                new Album()
                {
                    Id = 2,
                    Artist = "Habba babba",
                    Title = "Gumba gumba",
                    Country = "Suahili",
                    Tags = new[]{"emo", "folk", "ambient", "depressing", "dance", "party"}.ToList(),
                    ImagePath = @"https://townsquare.media/site/341/files/2012/05/1-Shut-Up-and-Dance-Dance-Before-the-Police-Come.jpg?w=980&q=75",
                    ReleaseDate = DateTime.Now.AddYears(-15),
                    Url = "dance.ugabuga.ru"
                },
                new Album()
                {
                    Id = 3,
                    Artist = "Elly Billish",
                    Title = "When we all wake up where do we run",
                    Country = "USA",
                    Tags = new[]{"kawaii", "teenagers", "drama", "pop", "sing under shower", "kidfriendly"}.ToList(),
                    ImagePath = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRSw-Hh-KfqHi7Vpm0n_mQsUADUE7QHRQGmjQ&usqp=CAU",
                    ReleaseDate = DateTime.Now.AddMonths(-6),
                    Url = "youtube.com"
                },
            };
        }

        public IEnumerable<Album> GetAllAlbums(int page = 1)
        {
            return _albumsRepository.Skip(PAGE_SIZE * (page - 1)).Take(PAGE_SIZE);
        }

        public IEnumerable<Album> GetAlbumsBySearchQuery(string searchQuery, int page=1)
        {
            return _albumsRepository.Skip(PAGE_SIZE * (page - 1)).Take(PAGE_SIZE);
        }
    }
}
