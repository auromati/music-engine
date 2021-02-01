using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IQueryParserService
    {
        QueryItems ParseQuery(string query);
    }
}