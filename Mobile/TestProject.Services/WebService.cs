using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class WebService : IWebService
    {
        //const string Url = "https://localhost:44366/api/todolist";
        const string Url = "http://10.10.3.215:3000/api/todolist";
        //const string Url = "https://10.10.3.215:44366/api/todolist";
        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<TodoItem>> Get()
        {
            HttpClient client = GetClient();
            string result = null;
            try
            {
                result = await client.GetStringAsync(Url);
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(result);
        }

        public async Task<TodoItem> Add(TodoItem todoItem)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(todoItem),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<TodoItem>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<TodoItem> Update(TodoItem todoItem)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + "/" + todoItem.Id,
                new StringContent(
                    JsonConvert.SerializeObject(todoItem),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<TodoItem>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<TodoItem> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<TodoItem>(
               await response.Content.ReadAsStringAsync());
        }

    }
}
