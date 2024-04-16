using ShopProject.UI.ViewModels.Pages.Manager;
using ShopProject.UI.ViewModels.Pages.SalesManager;
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

namespace ShopProject.UI.Views.Pages.SalesManager
{
    /// <summary>
    /// Логика взаимодействия для SMShopPage.xaml
    /// </summary>
    public partial class SMShopPage : Page
    {
        public SMShopsVM ViewModel { get; }
        public SMShopPage(
            SMShopsVM viewModel
            )
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            OnStartup();
        }
        private async void OnStartup()
        {
            await ViewModel.GetShopsCollectionCommand.ExecuteAsync(this);
            return;
        }
    }
}
