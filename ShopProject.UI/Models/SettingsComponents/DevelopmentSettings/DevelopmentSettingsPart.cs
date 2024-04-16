using ShopProject.UI.Models.SettingsComponents.APISettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class DevelopmentSettingsPart : ObservableObject
    {
        public DevelopmentSettingsPart()
        {
            InitDbCommand = new AsyncRelayCommand(InitDb);
            AutoLoginSettings = new AutoLoginSettings();
            FillDbSettings = new FillDbSettings();
        }

        private void SettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(sender)));
        }

        private AutoLoginSettings _autoLoginSettings;
        public AutoLoginSettings AutoLoginSettings
        {
            get => _autoLoginSettings;
            set
            {
                SetProperty(ref _autoLoginSettings, value);
                _autoLoginSettings.PropertyChanged += SettingsChanged;
            }
        }

        public IAsyncRelayCommand InitDbCommand { get; }
        private async Task InitDb()
        {
            await ClientDbProvider.InitDb();
        }

        
        private FillDbSettings _fillDbSettings;
        public FillDbSettings FillDbSettings
        {
            get => _fillDbSettings;
            set
            {
                SetProperty(ref _fillDbSettings, value);
                _fillDbSettings.PropertyChanged += SettingsChanged;
            }
        }
    }
}
