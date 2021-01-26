namespace WebAPI.Services
{
    public interface IAlbumService
    {
        System.Collections.Generic.IEnumerable<Models.Album> GetAlbums(int page = 1);
    }
}