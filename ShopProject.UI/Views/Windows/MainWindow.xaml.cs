using ShopProject.UI.Services.Contracts;
using ShopProject.UI.ViewModels.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;

namespace ShopProject.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {
        private INavigationService _navigationService;

        public MainWindowVM ViewModel { get; }
        public MainWindow(
            MainWindowVM viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider
            )
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);

            NavigationView.SetServiceProvider(serviceProvider);

            _navigationService = navigationService;
            
            
        }

        private bool _isUserClosedPane;

        private bool _isPaneOpenedOrClosedFromCode;
        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_isUserClosedPane)
            {
                return;
            }

            _isPaneOpenedOrClosedFromCode = true;
            NavigationView.IsPaneOpen = !(e.NewSize.Width <= 1200);
            _isPaneOpenedOrClosedFromCode = false;
        }

        private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = false;
        }

        private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = true;
        }

    }
}