using Newtonsoft.Json.Linq;
using ShopProject.UI.Models.SettingsComponents.AlertSettings;
using ShopProject.UI.Models.SettingsComponents.APISettings;
using ShopProject.UI.Models.SettingsComponents.DevelopmentSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents
{
    public partial class SettingsModel : ObservableObject
    {
        public SettingsModel()
        {
            APISettingsPart = new APISettingsPart();
            AlertSettingsPart = new AlertSettingsPart();
            DevelopmentSettingsPart = new DevelopmentSettingsPart();
        }
        private void SettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(sender)));
        }


        private APISettingsPart _aPISettingsPart;
        public APISettingsPart APISettingsPart
        {
            get => _aPISettingsPart;
            set
            {
                SetProperty(ref _aPISettingsPart, value);
                _aPISettingsPart.PropertyChanged += SettingsChanged;
            }         
        }
        private AlertSettingsPart _alertSettingsPart;
        public AlertSettingsPart AlertSettingsPart
        {
            get => _alertSettingsPart;
            set
            {
                SetProperty(ref _alertSettingsPart, value);
                _alertSettingsPart.PropertyChanged += SettingsChanged;
            }
        }

        private DevelopmentSettingsPart _developmentSettingsPart;

        public DevelopmentSettingsPart DevelopmentSettingsPart
        {
            get => _developmentSettingsPart;
            set
            {
                SetProperty(ref _developmentSettingsPart, value);
                _developmentSettingsPart.PropertyChanged += SettingsChanged;
            }
        }
    }
}
