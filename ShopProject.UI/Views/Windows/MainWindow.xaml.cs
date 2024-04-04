using ShopProject.UI.Services.Contracts;
using ShopProject.UI.ViewModels.Windows;
using ShopProject.UI.Views.Pages.Examples;
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

        private CacheStorageService _cacheStorageService;
        public MainWindowVM ViewModel { get; }
        public MainWindow(
            MainWindowVM viewModel,
            CacheStorageService cacheStorageService,
            INavigationService navigationService,
            IServiceProvider serviceProvider
            )
        {
            _cacheStorageService = cacheStorageService;
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);

            NavigationView.SetServiceProvider(serviceProvider);

            _navigationService = navigationService;
        }

        private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not NavigationView navigationView)
            {
                return;
            }

            NavigationView.HeaderVisibility =
                navigationView.SelectedItem?.TargetPageType != typeof(ProfilePage)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
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

        private void SelectShop(object sender, RoutedEventArgs e)
        {
            _cacheStorageService.SelectedShop = Settings.GetActiveUser().Shop;
            _navigationService.Navigate(typeof(ShopPage));
        }
    }
}