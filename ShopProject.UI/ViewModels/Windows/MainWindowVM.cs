

using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace ShopProject.UI.ViewModels.Windows
{
    public partial class MainWindowVM : ObservableObject
    {
        public Settings Settings { get;}
        public MainWindowVM()
        {
            Settings.LoadInstance();
            Settings = Settings.GetInstance();
            _alertPoster = AlertPoster.GetInstance();
            Settings.Exit();
            Settings.SettingsModel.APISettingsPart.APILoginSettings.TestConnectionCommand.Execute(this);
        }
        
        [ObservableProperty]
        private AlertPoster _alertPoster;

    }
}
