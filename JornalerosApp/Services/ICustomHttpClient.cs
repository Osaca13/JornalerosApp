using System.Net.Http;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public interface ICustomHttpClient
    {
        Task<T> GetJsonAsync<T>(string requestUri);
        Task<HttpResponseMessage> PostJsonAsync<T>(string requestUri, T content);
        Task<HttpResponseMessage> PutJsonAsync<T>(string requestUri, T content);
    }
}