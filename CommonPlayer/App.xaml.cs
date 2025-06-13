using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;

using CommonPlayer.ViewModels;
using CommonPlayer.Core.Services;
using CommonPlayer.Views;

namespace CommonPlayer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region Public fields
    public static readonly double MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
    public static readonly double MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    #endregion


    #region Enter point
    [STAThread]
    private static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }
    #endregion


    #region Private methods
    private static async Task MainAsync(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        await host.StartAsync().ConfigureAwait(true);

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();

        await host.StopAsync().ConfigureAwait(true);
    }
    #endregion


    #region Public methods
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
            => configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<PlayerView>();
            services.AddSingleton<PlayerViewModel>();
            services.AddSingleton<SettingsView>();
            services.AddSingleton<SettingsViewModel>();


            services.AddSingleton<INavigationService, NavigationService>();
            
            services.AddSingleton<Func<Type, BaseViewModel>>(services => viewModelType => (BaseViewModel)services.GetRequiredService(viewModelType));

            services.AddSingleton<WeakReferenceMessenger>();
            services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

            services.AddSingleton(_ => Current.Dispatcher);

            services.AddTransient<ISnackbarMessageQueue>(provider => 
            {
                Dispatcher dispatcher = provider.GetRequiredService<Dispatcher>();
                return new SnackbarMessageQueue(TimeSpan.FromSeconds(3.0), dispatcher);
            });
        });
    #endregion
}