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

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class ProfileVM : ObservableObject
    {

        [ObservableProperty]
        private string _login = "";

        [ObservableProperty]
        private string _password = "";

        public ProfileVM()
        {
            SingInCommand = new AsyncRelayCommand<object>((param) => SingIn(param));

        }

        public IAsyncRelayCommand SingInCommand { get; }
        private async Task SingIn(object passwordBoxObj)
        {
            if(passwordBoxObj == null || passwordBoxObj as PasswordBox == null)
            {
                return;
            }
            var response = await ClientDbProvider.SingIn(Login, (passwordBoxObj as PasswordBox).Password);
            if( response.IsSuccessStatusCode == false)
            {
                return;
            }
            var jsonUser = response.Content.ReadAsStream();
            var result = JsonSerializer.Deserialize<User>(jsonUser, JsonOptions.GetOptions());
            EntityList<Role> roles = new EntityList<Role>();
            await roles.Fill();
            result.Role = roles.FirstOrDefault(x => x.RoleId == result.RoleId);
            Settings.SetActiveUser(result);
        }


    }
}
