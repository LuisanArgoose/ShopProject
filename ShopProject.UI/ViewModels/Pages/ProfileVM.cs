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

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class ProfileVM : ObservableObject
    {
        //[ObservableProperty]
        //private IEntityList _table;

        [ObservableProperty]
        private string _login = null!;

        [ObservableProperty]
        private string _password = null!;

        public ProfileVM()
        {
            SingInCommand = new AsyncRelayCommand(SingIn);

        }

        public IAsyncRelayCommand SingInCommand { get; }
        private async Task SingIn()
        {

            await Task.Run(() =>
            {

            });
        }


    }
}
