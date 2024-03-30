using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopProject.UI.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ActiveProfilePage.xaml
    /// </summary>
    public partial class ActiveProfilePage : Page
    {
        public ActiveProfileVM ViewModel { get; }

        public ActiveProfilePage(
            ActiveProfileVM viewModel
            )
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
