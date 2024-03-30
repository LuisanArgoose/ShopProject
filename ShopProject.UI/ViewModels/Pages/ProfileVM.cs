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
            if (autoLoginSettings.IsActive)
            {
                Login = autoLoginSettings.Login;
                var passwordBox = new PasswordBox();
                passwordBox.Password = autoLoginSettings.Password;
                SingInCommand.Execute(passwordBox);
                
            }
            
        }


        public IAsyncRelayCommand SingInCommand { get; }
        private async Task SingIn(object passwordBoxObj)
        {
            if (passwordBoxObj == null || passwordBoxObj as PasswordBox == null)
            {
                return;
            }
            var response = await ClientDbProvider.SingIn(Login, (passwordBoxObj as PasswordBox).Password);
            if( response.IsSuccessStatusCode == false)
            {
                AlertPoster.PostErrorAlert("Вход в систему", "Ошибка входа");
                return;
            }
            var jsonUser = response.Content.ReadAsStream();
            var result = JsonSerializer.Deserialize<User>(jsonUser, JsonOptions.GetOptions());
            EntityList<Role> roles = new EntityList<Role>();
            await roles.Fill();
            result.Role = roles.FirstOrDefault(x => x.RoleId == result.RoleId);
            Settings.SetActiveUser(result);
            _navigationService.Navigate(typeof(ActiveProfilePage));
            AlertPoster.PostSuccessAlert("Вход в систему", "Успешный вход");
        }



    }
}
