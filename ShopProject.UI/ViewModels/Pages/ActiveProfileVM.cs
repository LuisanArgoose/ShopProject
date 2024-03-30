using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class ActiveProfileVM : ObservableObject
    {
        private INavigationService _navigationService;
        public ActiveProfileVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ActiveUser = Settings.GetActiveUser();
        }
        [RelayCommand]
        public void Exit()
        {
            Settings.Exit();
            _navigationService.Navigate(typeof(ProfilePage));
        }

        [ObservableProperty]
        private User _activeUser;
    }
}
