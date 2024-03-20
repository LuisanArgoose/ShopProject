using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class TokenLogin : ObservableObject
    {
        [ObservableProperty]
        private int _tokenLoginId;

        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _password;
    }
}
