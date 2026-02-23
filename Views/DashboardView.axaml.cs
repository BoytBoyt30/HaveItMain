using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HaveItMain.ViewModels;

namespace HaveItMain.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
        DataContext = new Dashboard();
    }
    
    private Dashboard ViewModel => DataContext as Dashboard;

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        TaskItemViewModel test = new TaskItemViewModel("Sample", new DateTime(2006, 4, 24), "not urgent", false);
        ViewModel?.AddTask(test);
    }
}