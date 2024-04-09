
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Models;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class SettingsVM : ObservableObject
    {
        [ObservableProperty]
        private Settings _settings = Settings.GetInstance();
        public SettingsVM()
        {
            SaveSettingsCommand = new AsyncRelayCommand(SaveSettings);
            LoadSettingsCommand = new AsyncRelayCommand(LoadSettings);
            Settings.LoadInstance();
            _settings.PropertyChanged += OnChange;
            
        }
        [ObservableProperty]
        private bool _isSomeChange;

        private void OnChange(object? sender, PropertyChangedEventArgs e)
        {
            IsSomeChange = true;
        }

        public IAsyncRelayCommand SaveSettingsCommand { get; }

        private async Task SaveSettings()
        {
            await Task.Run(() =>
            {
                Settings.SaveInstance();
                AlertPoster.PostSuccessAlert("Настройки сохранены");
                IsSomeChange = false;
            });
            
        }

        
        public IAsyncRelayCommand LoadSettingsCommand { get; }

        private async Task LoadSettings()
        {
            await Task.Run(() =>
            {
                Settings.LoadInstance();
                AlertPoster.PostInformationAlert("Настройки сброшены");
                IsSomeChange = false;
            });

        }
    }
}
