using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;


namespace ShopProject.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>  
    public partial class App : Application
    {
        private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(AppContext.BaseDirectory);
        })
        .ConfigureServices(
            (_, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Main window container with navigation
                services.AddSingleton<IWindow, MainWindow>();
                services.AddSingleton<MainWindowVM>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<WindowsProviderService>();

                // Top-level pages

                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsVM>();

                // All other pages and view models
                services.AddTransientFromNamespace("ShopProject.UI.Views", UIAssembly.Asssembly);
                services.AddTransientFromNamespace("ShopProject.UI.ViewModels", UIAssembly.Asssembly);
            }
        )
        .Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetRequiredService<T>()
            where T : class
        {
            return _host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private void OnExit(object sender, ExitEventArgs e)
        {
            _host.StopAsync().Wait();

            _host.Dispose();
        }

        
    }

}
