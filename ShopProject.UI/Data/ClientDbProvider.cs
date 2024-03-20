using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ShopProject.UI.Data
{
    public class ClientDbProvider
    {
        private static Settings _settings = Settings.GetInstance();

        private static HttpClient _httpClient;
        static ClientDbProvider()
        {
            
            string baseAddress = "https://localhost:7178/api/";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            
        }

        public static void SetUrl(string uri)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        public static async Task<HttpResponseMessage> TestConnect(string login, string password)
        {
            try
            {
                SetUrl(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url);
                var url = "Auth?login=" + login + "&password=" + password;
                var response = await _httpClient.GetAsync(url);
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                if (response.IsSuccessStatusCode)
                    AlertPoster.PostSystemSuccessAlert("Подключение к API");
                else
                    AlertPoster.PostSystemErrorAlert("Подключение к API", response.StatusCode.ToString());
                return response;
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Подключение к API", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }



        }

        public static async Task<HttpResponseMessage> GetEntitiesNameAsync()
        {
            try
            {
                SetUrl(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url);
                var url = "ServerDb/SelectEntitiesName";
                var response = await _httpClient.GetAsync(url);
                if(response.IsSuccessStatusCode)
                    AlertPoster.PostSystemSuccessAlert("Получение имён Entity");
                else
                    AlertPoster.PostSystemErrorAlert("Получение имён Entity", response.StatusCode.ToString());
                return response;
            }
            catch(Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Получение имён Entity", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            

        }
        public static async Task<HttpResponseMessage> GetEntitiesAsync(Type entityType)
        {
            
            try
            {
                SetUrl(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url);
                var url = "ServerDb/Select?entityTypeName=" + entityType.Name;
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    AlertPoster.PostSystemSuccessAlert("Получение Entity");
                else
                    AlertPoster.PostSystemErrorAlert("Получение Entity", response.StatusCode.ToString());
                return response;
            }
            catch(Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Получение Entity", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            

        }

        
        private static StringContent ComplectEntity(object entity)
        {
            string jsonEntity = JsonSerializer.Serialize(entity);
            string entityTypeName = entity.GetType().Name;
            EntityModel requestData = new EntityModel
            {
                JsonEntity = jsonEntity,
                EntityTypeName = entityTypeName

            };
            string requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            return new StringContent(requestDataJson, Encoding.UTF8, "application/json");

        }

        public static async Task<HttpResponseMessage> PostCUD(object entity, string operationName) 
        {

            List<string> operations =
            [
                "Create",
                "Delete",
                "Update"
            ];
            if (!operations.Contains(operationName))
                throw new Exception("Bad operation name");
            var content = ComplectEntity(entity);
            SetUrl(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url);
            var url = "ServerDb/" + operationName;
            var response = await _httpClient.PostAsync(url, content);
            return response;
        }
    }
}
