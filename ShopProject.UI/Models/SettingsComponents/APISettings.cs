using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.UI.Models.SettingsComponents
{
    public partial class APISettings : ObservableObject
    {
        public APISettings() 
        {
            try{
                Properties.Settings.Default.Имя_настройки = "значение";
            }
            catch
            {

            }
        }

        [ObservableProperty]
        private APILoginSettings _apiLoginSettings = null!;


        // Сделать сохранение логина и пароля через Settings
        // Сохранение значения
        //Properties.Settings.Default.Имя_настройки = "значение";
        // Сохранение изменений
        //Properties.Settings.Default.Save();
        // Получение значения
        //string значение = Properties.Settings.Default.Имя_настройки;
        public IAsyncRelayCommand SaveApiLoginSettingsCommand { get; }
    }
}
