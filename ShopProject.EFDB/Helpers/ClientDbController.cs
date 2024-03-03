using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProject.EFDB.Helpers
{
    public class ClientDbController
    {
        private readonly HttpClient _httpClient;
        public ClientDbController(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
        public async Task<IEnumerable<object>?> GetEntitiesAsync(Type entityType)
        {
            string jsonObject = JsonConvert.SerializeObject(entityType);
            var encodedJson = Uri.EscapeDataString(jsonObject);
            var url = "Db/Select?Json=" + encodedJson;
            var responseEntityCollection = await _httpClient.GetAsync(url);
            if (responseEntityCollection.IsSuccessStatusCode)
            {
                var jsonEntityCollection = await responseEntityCollection.Content.ReadAsStringAsync();
                var entityList = JsonConvert.DeserializeObject(jsonEntityCollection, typeof(List<>).MakeGenericType(entityType)) as IEnumerable<object>;
                return entityList;
            }
            else
            {
                return null;
            }
        }
        /*
        public async Task SendObjectToApi<T>(T entity)
        {
            string apiUrl = "http://localhost:5555/api/create/";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Object sent successfully to API");
            }
            else
            {
                Console.WriteLine("Error sending object to API. Status code: " + response.StatusCode);
            }
        }*/
        public async Task PostCreate()
        {

        }
    }
}
