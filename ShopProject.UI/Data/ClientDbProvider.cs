using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using ShopProject.EFDB.Models;
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Helpers;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata;
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


        private static async Task<HttpResponseMessage> GetAlertDecorator(string url, string message)
        {
            return await AlertDecorator(url, message, async (client) => await client.GetAsync(url));
        }
        private static async Task<HttpResponseMessage> PostAlertDecorator(string url, StringContent content, string message)
        {
            return await AlertDecorator(url, message, async (client) => await client.PostAsync(url, content));
        }
        private static async Task<HttpResponseMessage> AlertDecorator(string url, string message, Func<HttpClient, Task<HttpResponseMessage>> clientMethod)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    
                    var response = await clientMethod(client); ;
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
            //if (authorize && _token == null)
                //AlertPoster.PostSystemInformationAlert("Авторизация отключена");
            if(_token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
                
            return client;
            
        }

        public static async Task<HttpResponseMessage> SingIn(string login, string password)
        {
            var url = "ServerDb/SingIn?login=" + login + "&password=" + password;
            var response = await GetAlertDecorator(url, "Вход в систему");
            if (!response.IsSuccessStatusCode)
            {
                _token = null;
            }
            return response;
            

        }

        public static async Task<HttpResponseMessage> TestConnect(string login, string password)
        {
            var url = "Auth?login=" + login + "&password=" + password;
            var response = await GetAlertDecorator(url, "Подключение к API");
            if (response.IsSuccessStatusCode)
            {
                var jsonToken = await response.Content.ReadAsStringAsync().WaitAsync(CancellationToken.None);

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
            var response = await GetAlertDecorator(url, "Создание стартовых данных");
            return response;
        }
        public static async Task<HttpResponseMessage> ClearDb()
        {
            var url = "ServerDb/ClearDataBase";
            var response = await GetAlertDecorator(url, "Очистка базы данных");
            return response;
        }

        public static async Task<HttpResponseMessage> FillDb(DateTime startDate, DateTime endDate)
        {
            var url = "ServerDb/FillDataBase?startDate=" + startDate.ToString("MM.dd.yyyy") + "&endDate=" + endDate.ToString("MM.dd.yyyy");
            var response = await GetAlertDecorator(url, "Создание тестовых данных");
            return response;
        }

        public static async Task<HttpResponseMessage> GetMainShopPlan(int shopId, DateTime startDate, DateTime endDate)
        {
            var url = "ServerDb/GetMainShopPlan?shopId=" + shopId + "&startDate=" + startDate.ToString("MM.dd.yyyy")
                        + "&endDate=" + endDate.ToString("MM.dd.yyyy");
            var response = await GetAlertDecorator(url, "Получение общего плана");
            return response;
           
        }


        public static async Task<HttpResponseMessage> GetShopInfo(int shopId)
        {
            var url = "ServerDb/GetShopInfo?shopId=" + shopId;
            var response = await GetAlertDecorator(url, "Получение данных магазина");
            return response;
            
        }

        public static async Task<HttpResponseMessage> GetShopsCollection()
        {
            var url = "ServerDb/GetShopsCollection";
            var response = await GetAlertDecorator(url, "Получение списка магазинов");
            return response;

        }

        public static async Task<HttpResponseMessage> GetMainShopInfo(int shopId, int daysInterval)
        {
            var url = "ServerDb/GetMainShopInfo?shopId=" + shopId + "&daysInterval=" + daysInterval;
            var response = await GetAlertDecorator(url, "Получение основных данных магазина");
            return response;
        }

        // Получение списка метрик
        public static async Task<HttpResponseMessage> GetMetricsCollection()
        {
            var url = "ServerDb/GetMetricsCollection";
            var response = await GetAlertDecorator(url, "Получение списка метрик");
            return response;
            
        }

        // Получение списка планов выбранного атрибута
        public static async Task<HttpResponseMessage> GetMetricShopPlansCollection(int shopId, int metricId, int daysInterval)
        {
            var url = "ServerDb/GetMetricShopPlansCollection?shopId=" + shopId + "&metricId=" + metricId +
                "&daysInterval=" + daysInterval;
            var response = await GetAlertDecorator(url, "Получение списка планов по метрике"); 
            return response;
        }

        // Получение списка данных выбранного атрибута
        public static async Task<HttpResponseMessage> GetMetricPlanDataCollection(int shopId, int metricId, int daysInterval)
        {
            var url = "ServerDb/GetMetricPlanDataCollection?shopId=" + shopId + "&metricId=" + metricId +
                "&daysInterval=" + daysInterval;
            var response = await GetAlertDecorator(url, "Получение списка данных по метрике");
            return response;
        }
        public static async Task<HttpResponseMessage> DeleteShopPlan(int shopPlanId)
        {
            var url = "ServerDb/DeleteShopPlan?shopPlanId=" + shopPlanId;
            var response = await GetAlertDecorator(url, "Удаление плана");
            return response;
        }

        public static async Task<HttpResponseMessage> AddShopPlan(ShopPlan shopPlan)
        {
            string requestDataJson = JsonSerializer.Serialize<ShopPlan>(shopPlan);
            var content = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
            var url = "ServerDb/AddShopPlan";
            var response = await PostAlertDecorator(url, content, "Добавление плана");
            return response;
        }

        // Получение общего плана всех магазинов
        public static async Task<HttpResponseMessage> GetTotalMetricData(int daysInterval)
        {
            var url = "ServerDb/GetTotalMetricData?daysInterval=" + daysInterval;
            var response = await GetAlertDecorator(url, "Получение общего плана всех магазинов");
            return response;

        }
        public static async Task<HttpResponseMessage> GetTotalPlanData(int daysInterval)
        {
            var url = "ServerDb/GetTotalPlanData?daysInterval=" + daysInterval;
            var response = await GetAlertDecorator(url, "Получение успеваемости всех магазинов");
            return response;

        }
        
    }
}
