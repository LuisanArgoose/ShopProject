

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.UI.ViewModels.Windows
{
    public partial class MainWindowVM : ObservableObject
    {

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
