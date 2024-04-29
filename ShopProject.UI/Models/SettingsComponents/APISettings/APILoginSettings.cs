using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.APISettings
{
    public partial class APILoginSettings : ObservableObject
    {
        public APILoginSettings()
        {
            TestConnectionCommand = new AsyncRelayCommand(TestConnection);
        }
        
        [ObservableProperty]
        private string _login = null!;

        [ObservableProperty]
        private string _password = null!;


        [ObservableProperty]
        private string _url = null!;


        
        public IAsyncRelayCommand TestConnectionCommand { get; }

        public async Task TestConnection()
        {
            var result = await ClientDbProvider.TestConnect(Login, Password);
            if (result.IsSuccessStatusCode)
                AlertPoster.PostSuccessAlert("Подключение установлено");
            else
                AlertPoster.PostErrorAlert("Ошибка подключения");
            return;

        }
    }
}
