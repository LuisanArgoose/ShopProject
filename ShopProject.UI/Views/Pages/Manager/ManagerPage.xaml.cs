using LiveChartsCore.SkiaSharpView.Painting;
using ShopProject.UI.ViewModels.Pages.Manager;
using SkiaSharp;
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

namespace ShopProject.UI.Views.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerVM ViewModel { get; }

        public ManagerPage(
            ManagerVM viewModel
            )
        {
            ViewModel = viewModel;
            DataContext = this;

            

            InitializeComponent();
            ShopChart.LegendTextPaint = new SolidColorPaint(new SKColor(255, 255, 255));
            OnStartup();
        }
        private async void OnStartup()
        {
            await ViewModel.SelectedShop.GetShopAverageBillCommand.ExecuteAsync(this);
            return;
        }
    }
}
