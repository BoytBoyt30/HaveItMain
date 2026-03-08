using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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

    private void DyslexicToggle(object? sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            // Use the global App resources
            if (Application.Current != null)
            {
                var res = Application.Current.Resources;
            
                if (tb.IsChecked == true)
                    res["AppFont"] = res["DyslexicFont"];
                else
                    res["AppFont"] = res["NunitoFont"];
            }
        }
    }
}