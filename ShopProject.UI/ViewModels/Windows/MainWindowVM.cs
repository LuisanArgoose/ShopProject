

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
            
        }
        
        [ObservableProperty]
        private AlertPoster _alertPoster;


        [ObservableProperty]
        private ICollection<object> _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Профиль", SymbolRegular.People16, typeof(ProfilePage))
        };

        [ObservableProperty]
        private ICollection<object> _footerMenuItems = new ObservableCollection<object>()
        {
            new NavigationViewItem("Настройки", SymbolRegular.Settings16, typeof(SettingsPage))
        };
    }
}
