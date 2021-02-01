using NUnit.Framework;
using System.Linq;
using WebAPI.Services;

namespace NUnitTestProject1
{
    public class Tests
    {
        [Test]
        public void TestMockRepository()
        {
            IAlbumService albumService = new MockAlbumService();

            var albums = albumService.GetAllAlbums(page: 1);

            Assert.IsTrue(albums.Count() > 0);
        }


        [Test]
        public void TestQueryParser()
        {
            IQueryParserService queryParserService = new QueryParserService();

            string query = "Happiest dubstep serene music in Italy more something something";

            var dd = queryParserService.ParseQuery(query);

            Assert.IsTrue(dd.Locations.Count > 0);
        }
    }
}