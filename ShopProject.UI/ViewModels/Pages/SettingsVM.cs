
using ShopProject.UI.Models;
using ShopProject.UI.Models.SettingsComponents;
using System;
using System.Collections.Generic;
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
            LoadSettingsCommand.Execute(this);
            
        }

        

        
        public IAsyncRelayCommand SaveSettingsCommand { get; }

        private async Task SaveSettings()
        {
            await Task.Run(() =>
            {
                Settings.SaveInstance();
                //OnPropertyChanged(nameof(Settings));
            });
            
        }
        public IAsyncRelayCommand LoadSettingsCommand { get; }

        private async Task LoadSettings()
        {
            await Task.Run(() =>
            {
                Settings.LoadInstance();
                //OnPropertyChanged(nameof(Settings));

            });

        }
    }
}
