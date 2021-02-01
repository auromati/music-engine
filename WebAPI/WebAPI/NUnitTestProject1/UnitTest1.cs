
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using NUnit.Framework;
using System.IO;
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
            IHostingEnvironment env = new MockHostingEnvironment();
            IQueryParserService queryParserService = new QueryParserService(env);

            string query = "Happiest dubstep serene music in Italy more something something";

            var dd = queryParserService.ParseQuery(query);

            Assert.IsTrue(dd.Locations.Count > 0);
        }

        internal class MockHostingEnvironment : IHostingEnvironment
        {
            public string ApplicationName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public IFileProvider ContentRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public string ContentRootPath { get => Directory.GetCurrentDirectory() + "\\..\\..\\.."; set => throw new System.NotImplementedException(); }
            public string EnvironmentName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public IFileProvider WebRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public string WebRootPath { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        }
    }
}