namespace WebAPI.Services
{
    public interface IAlbumService
    {
        System.Collections.Generic.IEnumerable<Models.Album> GetAlbumsBySearchQuery(string searchQuery, int page = 1);
        System.Collections.Generic.IEnumerable<Models.Album> GetAlbumsByTagsQuery(string tagsQuery, int page = 1);
        System.Collections.Generic.IEnumerable<Models.Album> GetAllAlbums(int page = 1);
    }
}