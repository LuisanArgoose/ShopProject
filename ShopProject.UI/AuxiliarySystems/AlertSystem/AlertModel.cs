using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.AuxiliarySystems.AlertSystem
{
    public partial class AlertModel : ObservableObject
    {
        public AlertModel()
        {

        }
        public AlertModel(string title, string message, string type)
        {
            Title = title;
            Message = message;
            Type = type;
        }
        [ObservableProperty]
        private string _title = null!;

        [ObservableProperty]
        private string _message = null!;

        [ObservableProperty]
        private string _type = null!;
    }
}
