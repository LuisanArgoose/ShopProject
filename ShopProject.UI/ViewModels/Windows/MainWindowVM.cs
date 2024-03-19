

using ShopProject.UI.Models.AlertSystemComponents;
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
        private Settings _settings;
        public MainWindowVM()
        {
            _settings = Settings.LoadInstance();
            _alertSystem = AlertSystem.GetInstance();
            _settings.APISettingsPart.APILoginSettings.TestConnectionCommand.ExecuteAsync(this);
            //AlertSystem.PostAlert("awd", "Awda", "Sucsess");
        }

        [ObservableProperty]
        private AlertSystem _alertSystem;


        [ObservableProperty]
        private ICollection<object> _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Профиль", SymbolRegular.People16, typeof(ProfilePage))
        };

        [ObservableProperty]
        private ICollection<object> _footerMenuItems = new ObservableCollection<object>()
        {
            new NavigationViewItem("Settings", SymbolRegular.Settings16, typeof(SettingsPage))
        };
    }
}
