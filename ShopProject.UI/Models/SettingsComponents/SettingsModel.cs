using ShopProject.UI.Models.SettingsComponents.AlertSettings;
using ShopProject.UI.Models.SettingsComponents.APISettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents
{
    public partial class SettingsModel : ObservableObject
    {
        public SettingsModel()
        {
            PropertyChanged += OnSomePropertyChanged;
        }
        private void OnSomePropertyChanged(object sender, EventArgs e)
        {

        }

        [ObservableProperty]
        private APISettingsPart _aPISettingsPart = new APISettingsPart();

        [ObservableProperty]
        private AlertSettingsPart _alertSettingsPart = new AlertSettingsPart();
    }
}
