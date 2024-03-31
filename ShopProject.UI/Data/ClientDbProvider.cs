using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using ShopProject.EFDB.Models;
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Helpers;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.UI.Data
{
    public class ClientDbProvider
    {
        private static Settings _settings = Settings.GetInstance();
        private static string _token = null!;
        static ClientDbProvider()
        {            
            
        }

        public static HttpClient MyHttpClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url)
            };
            
            if(_token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            return client;
            
        }

        public static async Task<HttpResponseMessage> SingIn(string login, string password)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/SingIn?login=" + login + "&password=" + password;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Вход в систему");
                    }


                    else
                        AlertPoster.PostSystemErrorAlert("Вход в систему", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Вход в систему", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

        }

        public static async Task<HttpResponseMessage> TestConnect(string login, string password)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "Auth?login=" + login + "&password=" + password;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Подключение к API");
                        var jsonToken = await response.Content.ReadAsStringAsync();
                        
                        var result = JsonSerializer.Deserialize<TokenModel>(jsonToken,JsonOptions.GetOptions());
                        _token = result.Token;
                    }
                        

                    else
                        AlertPoster.PostSystemErrorAlert("Подключение к API", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Подключение к API", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }



        }
        public static async Task<HttpResponseMessage> GetEntitiesAsync(Type entityType)
        {
            
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/Select?entityTypeName=" + entityType.Name;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                        AlertPoster.PostSystemSuccessAlert("Получение Entity");
                    else
                        AlertPoster.PostSystemErrorAlert("Получение Entity", response.StatusCode.ToString());
                    return response;
                };
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
            using (var client = MyHttpClient())
            {
                var url = "ServerDb/" + operationName;
                var response = await client.PostAsync(url, content);
                return response;
            };
        }
    }
}
