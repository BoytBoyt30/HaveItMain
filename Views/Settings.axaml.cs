using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HaveItMain.ViewModels;

namespace HaveItMain.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void IncreaseFont(object? sender, RoutedEventArgs e)
    {
        if (FontSizePicker != null && FontSizePicker.SelectedIndex < FontSizePicker.ItemCount - 1)
        {
            FontSizePicker.SelectedIndex++;
        }
    }
    
    private void DecreaseFont(object? sender, RoutedEventArgs e)
    {
        if (FontSizePicker != null && FontSizePicker.SelectedIndex > 0)
        {
            FontSizePicker.SelectedIndex--;
        }
    }
}