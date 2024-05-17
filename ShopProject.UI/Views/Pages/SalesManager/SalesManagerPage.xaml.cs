using ShopProject.UI.ViewModels.Pages.Manager;
using ShopProject.UI.ViewModels.Pages.SalesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopProject.UI.Views.Pages.SalesManager
{
    /// <summary>
    /// Логика взаимодействия для SalesManagerPage.xaml
    /// </summary>
    public partial class SalesManagerPage : Page
    {
        public SalesManagerVM ViewModel { get; }
        public SalesManagerPage(SalesManagerVM viewModel
            )
        {
            ViewModel = viewModel;
            DataContext = this;
            
            InitializeComponent();
        }
        private void ToggleButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                e.Handled = true; // Предотвращаем обработку события, если кнопка уже включена
            }
        }
    }
}
