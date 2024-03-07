using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProject.EFDB.Helpers
{
    public class ClientDbProvider
    {
        private static ClientDbProvider? _instance;
        public static ClientDbProvider GetInstance()
        {
            _instance ??= new ClientDbProvider();
            return _instance;
        }

        private readonly HttpClient _httpClient;
        private ClientDbProvider()
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

            var url = "ServerDb/Select?entityTypeName=" + entityType.Name;
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
        
        public static StringContent ComplectEntity(object entity)
        {
            string jsonEntity = JsonSerializer.Serialize(entity);
            var requestData = new
            {
                jsonEntity,
                entityTypeName = entity.GetType().Name

            };
            string requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            return new StringContent(requestDataJson, Encoding.UTF8, "application/json");

        }
        public async Task<string> PostCRD(object entity, string operationName)
        {

            List<string> operations = new()
            {
                "Create",
                "Delete",
                "Update"
            };
            if (!operations.Contains(operationName))
                throw new Exception("Bad operation name");
            var content = ComplectEntity(entity);
            var url = "ServerDb/" + operationName;
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "Request failed with status code: " + response.StatusCode;
            }
        }
       
    }
}
