

using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Models.SettingsComponents;
using ShopProject.UI.Views.Pages.Manager;
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

        [ObservableProperty]
        private ICollection<object> _managerMenuItems = new ObservableCollection<object>
        {

                new NavigationViewItem("Typography", SymbolRegular.TextFont24, typeof(ManagerPage)),
                new NavigationViewItem("Icons", SymbolRegular.Diversity24, typeof(ManagerPage)),
                new NavigationViewItem("Colors", SymbolRegular.Color24, typeof(ManagerPage))

        };
        public MainWindowVM()
        {
            Settings.LoadInstance();
            Settings = Settings.GetInstance();
            _alertPoster = AlertPoster.GetInstance();
            Settings.SettingsModel.APISettingsPart.APILoginSettings.TestConnectionCommand.Execute(this);
        }
        
        [ObservableProperty]
        private AlertPoster _alertPoster;

    }
}
