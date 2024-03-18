using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models
{
    internal partial class APILoginSettings : ObservableObject
    {
        [ObservableProperty]
        private string _login = null!;

        [ObservableProperty]
        public string _password = null!;

        [ObservableProperty]
        public string _url = null!;
    }
}
