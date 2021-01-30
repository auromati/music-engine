namespace WebAPI.Services
{
    public interface IQueryParserService
    {
        string[] ParseQuery(string query);
    }
}