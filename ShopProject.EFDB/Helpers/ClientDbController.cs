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
        private static ClientDbController? _instance;
        public static ClientDbController GetInstance()
        {
            _instance ??= new ClientDbController();
            return _instance;
        }

        private readonly HttpClient _httpClient;
        private ClientDbController()
        {
            string baseAddress = "https://localhost:7178/api/";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
        public void SetUri(string uri)
        {
            _httpClient.BaseAddress = new Uri(uri); 
        }
        public async Task<string?> GetEntitiesAsync(Type entityType)
        {

            var url = "ServerDb/Select?tableType=" + entityType.Name;
            var responseEntityCollection = await _httpClient.GetAsync(url);

            if (responseEntityCollection.IsSuccessStatusCode)
            {
                var jsonEntityCollection = await responseEntityCollection.Content.ReadAsStringAsync();

                return jsonEntityCollection;
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
