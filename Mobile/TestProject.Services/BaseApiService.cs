using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using TestProject.ApiModels.User;
using TestProject.Configurations;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class BaseApiService : IBaseApiService
    {
        protected string _url;

        protected IStorageHelper _storage;

        protected DateTime TokenExpirationDate;

        public BaseApiService(IStorageHelper storage)
        {
            _storage = storage;
        }

        public async Task<T> Get<T>(string requestUri = null) where T : class
        {
            await RefreshToken();
            HttpClient client = await GetClient();
            if (string.IsNullOrEmpty(requestUri))
            {
                requestUri = $"{_url}";
            }
            string result = await client.GetStringAsync(requestUri);
            T entity = JsonConvert.DeserializeObject<T>(result);
            return entity;
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest entity, string requestUri,
            bool isAuthorizationRequired = true) where TResponse : class
        {
            await RefreshToken();
            HttpClient client = await GetClient(isAuthorizationRequired);
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
            await RefreshToken();
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
            await RefreshToken();
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

        protected async Task<HttpClient> GetClient(bool isAuhorizationRequired = true)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (!isAuhorizationRequired)
            {
                return client;
            }
            string token = await _storage.Get(Constants.AccessTokenKey);
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Bearer", token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        private async Task RefreshToken()
        {
            string expirationDate = await _storage.Get(Constants.ExpirationDateKey);
            bool isSuccess = DateTime.TryParse(expirationDate, out TokenExpirationDate);
            if (isSuccess && TokenExpirationDate > DateTime.Now)
            {
                return;
            }
            HttpClient client = await GetClient(false);
            string requestUri = "http://10.10.3.215:3000/api/userapi/RefreshToken";
            var requestRefreshToken = new RequestRefreshAccessTokenUserApiModel
            {
                AccessToken = await _storage.Get(Constants.AccessTokenKey),
                RefreshToken = await _storage.Get(Constants.RefreshTokenKey)
            };
            string content = JsonConvert.SerializeObject(requestRefreshToken);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await client.PostAsync(requestUri, stringContent);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return;
            }
            string serializedResponse = await httpResponse.Content.ReadAsStringAsync();
            ResponseRefreshAccessTokenUserApiModel responseRefreshToken = 
                JsonConvert.DeserializeObject<ResponseRefreshAccessTokenUserApiModel>(serializedResponse);
            RewriteTokenData(responseRefreshToken);
        }

        private async void RewriteTokenData(ResponseRefreshAccessTokenUserApiModel response)
        {
            _storage.Clear();
            await _storage.Save(Constants.AccessTokenKey, response.AccessToken);
            await _storage.Save(Constants.RefreshTokenKey, response.RefreshToken);
            await _storage.Save(Constants.ExpirationDateKey, response.TokenExpirationDate.ToString());
        }
    }
}
