using ShopProject.UI.Models.SettingsComponents.APISettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class DevelopmentSettingsPart : ObservableObject
    {
        [ObservableProperty]
        private AutoLoginSettings _autoLoginSettings = new AutoLoginSettings();
    }
}
