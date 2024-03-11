using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Services.Contracts
{
    public interface IWindow
    {
        event System.Windows.RoutedEventHandler Loaded;

        void Show();
    }
}
