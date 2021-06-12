using System.Threading.Tasks;

namespace Music.ConsoleApp.Interfaces
{
    public interface IHttpClientService<T> where T : class
    {
        Task<T> Get(string url);

        Task<T> Post(string apiUrl, T postObject);

        Task Put(string apiUrl, T putObject);
    }
}
