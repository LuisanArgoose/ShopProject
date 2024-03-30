using ShopProject.UI.AuxiliarySystems.ExpiringListSystem;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.UI.AuxiliarySystems.AlertSystem
{
    public partial class AlertPoster : ObservableObject
    {
        private static AlertPoster _instance = new AlertPoster();
        public static AlertPoster GetInstance() => _instance;

        private AlertPoster() { }

        [ObservableProperty]
        private ExpiringList<AlertModel> _alertModels = new ExpiringList<AlertModel>();

        public static void PostSystemSuccessAlert(string title, string message = "Успешно")
        {
            if(Settings.GetInstance().SettingsModel.AlertSettingsPart.ShowSystemAlerts)
                AddAlert(new AlertModel(title, "(Системное) " + message, "Success"));
        }
        public static void PostSystemErrorAlert(string title, string message = "")
        {
            if (Settings.GetInstance().SettingsModel.AlertSettingsPart.ShowSystemAlerts)
                AddAlert(new AlertModel(title, "(Системное) " + message, "Error"));
        }
        public static void PostSystemInformationrAlert(string title, string message = "")
        {
            if (Settings.GetInstance().SettingsModel.AlertSettingsPart.ShowSystemAlerts)
                AddAlert(new AlertModel(title, "(Системное) " + message, "Information"));
        }

        public static void PostSuccessAlert(string title, string message = "Успешно")
        {
            AddAlert(new AlertModel(title, message, "Success"));
        }
        public static void PostErrorAlert(string title, string message = "")
        {
            AddAlert(new AlertModel(title, message, "Error"));
        }
        public static void PostInformationrAlert(string title, string message = "")
        {
            AddAlert(new AlertModel(title, message, "Information"));
        }

        private static void AddAlert(AlertModel alertModel)
        {
            var intLifeTime = Settings.GetInstance().SettingsModel.AlertSettingsPart.AlertLifeTime;
            var lifeTime = TimeSpan.FromSeconds(intLifeTime);
            _instance.AlertModels.AddItem(alertModel, lifeTime);
        }
    }
}
