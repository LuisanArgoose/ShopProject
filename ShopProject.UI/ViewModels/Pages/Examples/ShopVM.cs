using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages.Examples
{
    public partial class ShopVM : ObservableObject
    {
        private INavigationService _navigationService;
        public ShopVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
