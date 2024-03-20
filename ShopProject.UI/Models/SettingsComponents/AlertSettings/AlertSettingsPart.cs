using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.AlertSettings
{
    public partial class AlertSettingsPart : ObservableObject
    {
        [ObservableProperty]
        private int _alertLifeTime = 5;

        [ObservableProperty]
        private bool _showSystemAlerts = false;
    }
}
