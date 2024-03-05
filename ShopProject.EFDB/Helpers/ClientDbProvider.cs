using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        
        public async Task PostCRD(string jsonEntity, string operationName)
        {
            List<string> operations = new()
            {
                "Create",
                "Delete",
                "Update"
            };
            if (!operations.Contains(operationName))
                throw new Exception("Bad operation name");
            var url = "ServerDb/" + operationName + "?tableType=" + jsonEntity;
        }
       
    }
}
