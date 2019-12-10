using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class BaseApiService : IBaseApiService
    {
        protected string _url;

        protected IStorageHelper _storage;

        public BaseApiService(IStorageHelper storage)
        {
            _storage = storage;
        }

        public async Task<T> Get<T>(string requestUri = null) where T : class
        {
            HttpClient client = await GetClient();
            if (string.IsNullOrEmpty(requestUri))
            {
                requestUri = $"{_url}";
            }
            string result = await client.GetStringAsync(requestUri);
            T entity = JsonConvert.DeserializeObject<T>(result);
            return entity;
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest entity, string requestUri) where TResponse : class
        {
            HttpClient client = await GetClient();
            string content = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUri, stringContent);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            TResponse deserializedObject = JsonConvert.DeserializeObject<TResponse>(serializedObject);
            return deserializedObject;
        }

        public async Task<T> Update<T>(T entity) where T : class
        {
            HttpClient client = await GetClient();
            string contentUri = $"{_url}";
            string content = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(contentUri, stringContent);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            T updatedEntity = JsonConvert.DeserializeObject<T>(serializedObject);
            return updatedEntity;
        }

        public async Task<T> Delete<T>(string requestUri = null) where T : class
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                requestUri = $"{_url}/Delete";
            }
            HttpClient client = await GetClient();
            var response = await client.DeleteAsync(requestUri);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        protected async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string token = await _storage.Get();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Bearer", token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }
    }
}
