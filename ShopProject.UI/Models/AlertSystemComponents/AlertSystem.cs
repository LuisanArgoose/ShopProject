using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.UI.Models.AlertSystemComponents
{
    public partial class AlertSystem : ObservableObject
    {
        private static AlertSystem _instance = new AlertSystem();
        public static AlertSystem GetInstance() => _instance;

        private AlertSystem() { }

        [ObservableProperty]
        private BindingList<AlertModel> _alertModels = new BindingList<AlertModel>();

        public static void PostAlert(HttpResponseMessage httpResponseMessage)
        {
            string type;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                type = "Success";
            }
            else
            {
                type = "Error";
            }
            AddAlert(new AlertModel("Подключение к API", "", type));
        }
        public static void PostAlert(string title, string message, string type)
        {
            AddAlert(new AlertModel(title, message, type));
        }

        private async static void AddAlert(AlertModel alertModel)
        {

            _instance.AlertModels.Add(alertModel);
             /*Application.Current.Dispatcher.Invoke(() =>
             {
                 _instance.AlertModels.Add(alertModel);
             });

             if (!_isKillerRun)
             {

                 await KillTimer();
             }*/
        }

        /*
        private static bool _isKillerRun = false;
        private static async Task KillTimer()
        {
            await Task.Run(() =>
            {
                _isKillerRun = true;
                while (_instance.AlertModels.Count > 0)
                {
                    Task.Delay(5000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _instance.AlertModels.RemoveAt(0);
                    });
                }
                _isKillerRun = false;
            });
        }*/
    }
}
