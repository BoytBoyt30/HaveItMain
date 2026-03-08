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
        if (FontSizePicker.SelectedIndex < FontSizePicker.ItemCount - 1)
        {
            FontSizePicker.SelectedIndex++;
        }
    }

    private void DecreaseFont(object? sender, RoutedEventArgs e)
    {
        if (FontSizePicker.SelectedIndex > 0)
        {
            FontSizePicker.SelectedIndex--;
        }
    }
    
    private void UpdateGlobalFontSize(double newSize)
    {
        if (Application.Current != null)
        {
            // Explicitly target the Application-level resource dictionary
            Application.Current.Resources["FontSizeNormal"] = newSize;
            Application.Current.Resources["FontSizeLarge"] = newSize * 2.25;
            
        }
    }
    private void FontSizePicker_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // If the control isn't fully 'attached' yet, ignore the event
        if (!this.IsInitialized) return; 

        if (FontSizePicker.SelectedItem is ComboBoxItem item)
        {
            string? val = item.Tag?.ToString() ?? item.Content?.ToString();
            if (double.TryParse(val, out double size))
            {
                UpdateGlobalFontSize(size);
            }
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