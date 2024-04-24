using ShopProject.UI.AuxiliarySystems.AlertSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.AuxiliarySystems
{
    public class AlertDeserializer
    {
       
        public static async Task<T?> Deserialize<T>(HttpResponseMessage response, string message)
        {
            try
            {
                if (response.IsSuccessStatusCode == false)
                {
                    AlertPoster.PostErrorAlert(message, "Не удалось получить данные");
                    return default;
                }
                var jsonShopInfo = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(jsonShopInfo);
                if (result == null)
                {
                    AlertPoster.PostSystemErrorAlert(message, "Не удалось сериализовать данные");
                    return default;
                }
                return result;
            }
            catch(Exception ex)
            {
                AlertPoster.PostSystemErrorAlert(message, ex.Message);
                return default;
            }
            
        }
    }
}
