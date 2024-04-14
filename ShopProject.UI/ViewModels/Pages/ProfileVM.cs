using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using ShopProject.UI.Data;
using ShopProject.UI.Helpers;
using ShopProject.UI.Models.SettingsComponents;
using ShopProject.EFDB.Models;
using System.Security;
using ShopProject.UI.AuxiliarySystems.AlertSystem;

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class ProfileVM : ObservableObject
    {
        private INavigationService _navigationService;

        [ObservableProperty]
        private string _login = "";

        public ProfileVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SingInCommand = new AsyncRelayCommand<object>((param) => SingIn(param));
            AutoLogin();


        }
        private void AutoLogin()
        {
            
            var autoLoginSettings = Settings.GetInstance().SettingsModel.DevelopmentSettingsPart.AutoLoginSettings;
            if (autoLoginSettings.IsActive && autoLoginSettings.IsFirst)
            {

                Login = autoLoginSettings.Login;
                var passwordBox = new PasswordBox();
                passwordBox.Password = autoLoginSettings.Password;
                SingInCommand.Execute(passwordBox);
                autoLoginSettings.IsFirst = false;
                
                
            }
            
        }

        [ObservableProperty]
        private bool _isLoading;
        public IAsyncRelayCommand SingInCommand { get; }
        private async Task SingIn(object passwordBoxObj)
        {
            try
            {
                IsLoading = true;
                await Settings.GetInstance().SettingsModel.APISettingsPart.APILoginSettings.TestConnection().WaitAsync(CancellationToken.None);
                
            }
            catch
            {
                IsLoading = false;
                AlertPoster.PostErrorAlert("Ошибка подключения");
                return;
            }
            if (passwordBoxObj == null || passwordBoxObj as PasswordBox == null)
            {
                IsLoading = false;
                return;
            }
            var response = await ClientDbProvider.SingIn(Login, (passwordBoxObj as PasswordBox).Password).WaitAsync(CancellationToken.None);
            if( response.IsSuccessStatusCode == false)
            {
                IsLoading = false;
                AlertPoster.PostErrorAlert("Вход в систему", "Ошибка входа");
                return;
            }
            var jsonUser = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<User>(jsonUser);
            Settings.SetActiveUser(result);
            //var res = _navigationService.GetNavigationControl();
            _navigationService.Navigate(typeof(ActiveProfilePage));
            AlertPoster.PostSuccessAlert("Вход в систему", "Успешный вход");
            IsLoading = false;
        }



    }
}
