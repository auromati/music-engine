using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IGeoNamesService
    {
        Location GetLocation(string url);
    }
}