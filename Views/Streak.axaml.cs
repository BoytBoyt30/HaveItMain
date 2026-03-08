using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HaveItMain.Services;
using HaveItMain.ViewModels;

namespace HaveItMain.Views;

public partial class StreakView : UserControl
{
    public StreakView()
    {
        InitializeComponent();
    }

    private async void Share(object? sender, RoutedEventArgs e)
    {
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

        if (clipboard != null)
        {
            string dummyLink = "https://haveit.app/streak/user123";
            await clipboard.SetTextAsync(dummyLink);
            NotificationService.Show("Copied link to Clipboard");
        }
    }
}