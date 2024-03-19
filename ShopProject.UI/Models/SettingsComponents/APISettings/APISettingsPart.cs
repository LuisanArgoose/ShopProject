using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.UI.Models.SettingsComponents.APISettings
{
    public partial class APISettingsPart : ObservableObject
    {

        [ObservableProperty]
        private APILoginSettings _aPILoginSettings = new APILoginSettings();

    }
}
