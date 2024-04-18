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
                    {
                        _token = null;
                        AlertPoster.PostSystemErrorAlert("Вход в систему", response.StatusCode.ToString());
                    }
                        
                    return response;
                };
            }
            catch (Exception ex)
            {
                _token = null;
                AlertPoster.PostSystemErrorAlert("Вход в систему", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

        }

        public static async Task<HttpResponseMessage> TestConnect(string login, string password)
        {
            try
            {
                using (var client = MyHttpClient(false))
                {
                    var url = "Auth?login=" + login + "&password=" + password;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Подключение к API");
                        var jsonToken = await response.Content.ReadAsStringAsync();
                        
                        var result = JsonSerializer.Deserialize<TokenModel>(jsonToken);
                        _token = result.Token;
                    }
                    else
                    {
                        AlertPoster.PostSystemErrorAlert("Подключение к API", response.StatusCode.ToString());
                    }
                        
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Подключение к API", ex.Message);
                _token = "Bad";
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }



        }
        public static async Task<HttpResponseMessage> InitDb()
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/InitializeDataBase";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Создание стартовых данных");
                    }
                    else
                        AlertPoster.PostSystemErrorAlert("Создание стартовых данных", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Создание стартовых данных", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<HttpResponseMessage> FillDb(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/FillDataBase?startDate=" + startDate.ToString() + "&endDate=" + endDate.ToString();
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Создание тестовых данных");
                    }
                    else
                        AlertPoster.PostSystemErrorAlert("Создание тестовых данных", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Создание тестовых данных", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
        public static async Task<HttpResponseMessage> GetShopAverageBill(int shopId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/GetShopAverageBill?shopId=" + shopId + "&startDate=" + startDate.ToString("MM.dd.yyyy") + "&endDate=" + endDate.ToString("MM.dd.yyyy");
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Получение графика магазина");
                    }
                    else
                        AlertPoster.PostSystemErrorAlert("Получение графика магазина", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Получение графика магазина", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<HttpResponseMessage> GetShopInfo(int shopId)
        {
            try
            {
                using (var client = MyHttpClient())
                {
                    var url = "ServerDb/GetShopInfo?shopId=" + shopId;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Получение данных магазина");
                    }
                    else
                        AlertPoster.PostSystemErrorAlert("Получение данных магазина", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Получение данных магазина", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<HttpResponseMessage> GetShopsCollection()
        {
            try
            {
                using (var client = MyHttpClient())
                {

                    var url = "ServerDb/GetShopsCollection";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        AlertPoster.PostSystemSuccessAlert("Получение списка магазинов");
                    }
                    else
                        AlertPoster.PostSystemErrorAlert("Получение списка магазинов", response.StatusCode.ToString());
                    return response;
                };
            }
            catch (Exception ex)
            {
                AlertPoster.PostSystemErrorAlert("Получение списка магазинов", ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }



    }
}
