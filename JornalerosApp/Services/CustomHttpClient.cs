using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace JornalerosApp.Services
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;
        public CustomHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> GetJsonAsync<T>(string requestUri)
        {
            var client = _httpClient;
            var httpContent = await client.GetAsync(requestUri);
            string jsonContent = httpContent.Content.ReadAsStringAsync().Result;
            T obj = JsonConvert.DeserializeObject<T>(jsonContent);
            httpContent.Dispose();
            client.Dispose();
            return obj;
        }
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string requestUri, T content)
        {
            var client = _httpClient;
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, stringContent);
            client.Dispose();
            return response;
        }
        public async Task<HttpResponseMessage> PutJsonAsync<T>(string requestUri, T content)
        {
            var client = _httpClient;
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(requestUri, stringContent);
            //client.Dispose();
            return response;
        }

    }
}
