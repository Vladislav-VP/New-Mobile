using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class BaseApiService<TEntity> : IBaseApiService<TEntity> where TEntity : BaseEntity
    {
        protected string _url;

        public async Task<IEnumerable<TEntity>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(_url);
            IEnumerable<TEntity> entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(result);
            return entities;
        }

        public async Task<TEntity> Get(int id)
        {
            HttpClient client = GetClient();
            string requestUri = $"{_url}/{id}";
            string result = await client.GetStringAsync(requestUri);
            TEntity entity = JsonConvert.DeserializeObject<TEntity>(result);
            return entity;
        }

        public async Task<TEntity> AddToApi(TEntity entity)
        {
            HttpClient client = GetClient();
            string content = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_url, stringContent);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            TEntity deserializedObject = JsonConvert.DeserializeObject<TEntity>(serializedObject);
            return deserializedObject;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            HttpClient client = GetClient();
            string contentUri = $"{_url}/{entity.Id}";
            string content = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(contentUri, stringContent);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            TEntity updatedEntity = JsonConvert.DeserializeObject<TEntity>(serializedObject);
            return updatedEntity;
        }

        public async Task<TEntity> Delete(int id)
        {
            HttpClient client = GetClient();
            string requestUri = $"{_url}/{id}";
            var response = await client.DeleteAsync(requestUri);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            string serializedObject = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(serializedObject);
        }

        protected HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
