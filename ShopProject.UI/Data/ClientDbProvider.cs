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
    public static class ClientDbProvider
    {
        private static Settings _settings = Settings.GetInstance();
        private static string? _token = null!;
        static ClientDbProvider()
        {            
            
        }

        private static async Task<HttpResponseMessage> AlertDecorator(string url, string message)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert(message);
                        return response;
                    }
                    else
                    {
                        
                        AlertPoster.PostSystemErrorAlert(message, response.StatusCode.ToString());
                        return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                        
                    }

                    
                };
            }
            catch (Exception ex)
            {
                
                AlertPoster.PostSystemErrorAlert(message, ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static HttpClient MyHttpClient(bool authorize = true)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.SettingsModel.APISettingsPart.APILoginSettings.Url)
            };
            if (authorize && _token == null)
                AlertPoster.PostSystemInformationAlert("Авторизация отключена");
            if(_token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            return client;
            
        }

        public static async Task<HttpResponseMessage> SingIn(string login, string password)
        {
            var url = "ServerDb/SingIn?login=" + login + "&password=" + password;
            var response = await AlertDecorator(url, "Вход в систему");
            if (!response.IsSuccessStatusCode)
            {
                _token = null;
            }
            return response;
            

        }

        public static async Task<HttpResponseMessage> TestConnect(string login, string password)
        {
            var url = "Auth?login=" + login + "&password=" + password;
            var response = await AlertDecorator(url, "Подключение к API");
            if (!response.IsSuccessStatusCode)
            {
                var jsonToken = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<TokenModel>(jsonToken);
                _token = result.Token;
            }
            else
            {
                _token = null;
            }
            return response;
           
        }
        public static async Task<HttpResponseMessage> InitDb()
        {
            var url = "ServerDb/InitializeDataBase";
            var response = await AlertDecorator(url, "Создание стартовых данных");
            return response;
        }

        public static async Task<HttpResponseMessage> FillDb(DateTime startDate, DateTime endDate)
        {
            var url = "ServerDb/FillDataBase?startDate=" + startDate.ToString() + "&endDate=" + endDate.ToString();
            var response = await AlertDecorator(url, "Создание тестовых данных");
            return response;
        }

        public static async Task<HttpResponseMessage> GetMainShopPlan(int shopId, DateTime startDate, DateTime endDate)
        {
            var url = "ServerDb/GetMainShopPlan?shopId=" + shopId + "&startDate=" + startDate.ToString("MM.dd.yyyy")
                        + "&endDate=" + endDate.ToString("MM.dd.yyyy");
            var response = await AlertDecorator(url, "Получение общего плана");
            return response;
           
        }


        public static async Task<HttpResponseMessage> GetShopInfo(int shopId)
        {
            var url = "ServerDb/GetShopInfo?shopId=" + shopId;
            var response = await AlertDecorator(url, "Получение данных магазина");
            return response;
            
        }

        public static async Task<HttpResponseMessage> GetShopsCollection()
        {
            var url = "ServerDb/GetShopsCollection";
            var response = await AlertDecorator(url, "Получение списка магазинов");
            return response;

        }

        // Получение списка доступных атрибутов
        public static async Task<HttpResponseMessage> GetPlanAtributesCollection()
        {
            var url = "ServerDb/GetPlanAtributesCollection";
            var response = await AlertDecorator(url, "Получение списка доступных атрибутов");
            return response;
            
        }

        // Получение списка планов выбранного атрибута
        public static async Task<HttpResponseMessage> GetAtributedShopPlansCollection(int shopId, int planAtributeId, DateTime endDate, DateTime startDate)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway);
        }

        // Получение списка данных выбранного атрибута
        public static async Task<HttpResponseMessage> GetAtributeObjectsCollection(int shopId, int planAtributeId, DateTime endDate, DateTime startDate)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway);
        }
    }
}
