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
    }
}