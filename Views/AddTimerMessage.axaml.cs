using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HaveItMain.ViewModels;

namespace HaveItMain.Views;

public partial class AddTimerMessage : Window
{
    public TaskItemViewModel? PrefillTask { get; set; } // reference the old data of the task, so it can be the new timer
    
    public AddTimerMessage()
    {
        InitializeComponent();
        
        this.Opened += (_, _) =>
        {
            if (PrefillTask == null) return;

            TaskTitleBox.Text = PrefillTask.Title;
        }; 
    }
    
    public async Task ShowDialogAsync(Window owner)
    {
        await ShowDialog(owner);
    }
    
    private void Discard(object? sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void Add(object? sender, RoutedEventArgs e)
    {
        var title = TaskTitleBox.Text ?? "New Timer";
        var duration = TimeSpan.FromMinutes(5);

        var task = new TimerViewModel(
            title,
            duration
        );
        
        task.Start();

        Close(task);
    }
}