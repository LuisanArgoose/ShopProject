using ShopProject.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class AutoLoginSettings : ObservableObject
    {

        [ObservableProperty]
        private bool _isActive = false;

        [ObservableProperty]
        private string _login = null!;

        [ObservableProperty]
        private string _password = null!;


        private bool _isFirst;

        [SoapIgnore]
        public bool IsFirst
        {
            get => _isFirst;
            set => SetProperty(ref _isFirst, value);
        } 

    }
}
