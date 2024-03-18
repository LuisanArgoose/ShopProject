
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
        public SettingsVM()
        {

        }

        [ObservableProperty]

        // Вывод настроек API
        private APISettings _apiSettings = null!;


        
    }
}
