using ShopProject.UI.AuxiliarySystems.AlertSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.AlertSettings
{
    public partial class AlertSettingsPart : ObservableObject
    {
        public AlertSettingsPart()
        {
            TestAlertCommand = new RelayCommand(TestAlert);

        }



        [ObservableProperty]
        private int _alertLifeTime = 5;

        [ObservableProperty]
        private bool _showSystemAlerts = false;



        public IRelayCommand TestAlertCommand { get; }

        private void TestAlert()
        {
            AlertPoster.PostSuccessAlert("Тестовое уведомление");
        }
    }
}
