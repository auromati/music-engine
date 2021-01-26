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

            var albums = albumService.GetAlbums(page: 1);

            Assert.IsTrue(albums.Count() > 0);
        }
    }
}