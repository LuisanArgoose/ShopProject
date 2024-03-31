using ShopProject.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class AutoLoginSettings : ObservableObject
    {

        [ObservableProperty]
        private bool _isActive = false;

        [ObservableProperty]
        private string _login = null!;

        [ObservableProperty]
        private string _password = null!;

        
    }
}
