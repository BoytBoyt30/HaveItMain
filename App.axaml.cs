using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HaveItMain.Services;
using HaveItMain.ViewModels;
using HaveItMain.Views;
using Microsoft.Toolkit.Uwp.Notifications;

namespace HaveItMain;

public partial class App : Application
{
    
    public static AppState ServiceState { get; set; } = new();
    public override void Initialize()
    {
        ToastNotificationManagerCompat.History.Clear();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ServiceState.NotificationService = new WindowsNotificationService();
            var sessionService = new SessionService();
            var session = sessionService.LoadSession();

            var viewModel = new MainWindowViewModel(ServiceState);

            if (session.IsSignedIn)
            {
                desktop.MainWindow = new MainWindow { DataContext = viewModel };
            }
            else
            {
                desktop.MainWindow = new Landing { DataContext = viewModel };
            }
        }
        base.OnFrameworkInitializationCompleted();
    }
}