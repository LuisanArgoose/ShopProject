using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.UI.Models.SettingsComponents.APISettings
{
    public partial class APISettingsPart : ObservableObject
    {
        public APISettingsPart()
        {
            APILoginSettings = new APILoginSettings();
        }

        private void SettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(sender)));
        }
        private APILoginSettings _aPILoginSettings;
        public APILoginSettings APILoginSettings
        {
            get => _aPILoginSettings;
            set
            {
                SetProperty(ref _aPILoginSettings, value);
                _aPILoginSettings.PropertyChanged += SettingsChanged;
            }
        }

    }
}
