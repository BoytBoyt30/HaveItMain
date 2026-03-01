using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using HaveItMain.ViewModels;

namespace HaveItMain.Views;

public partial class DashboardView : UserControl
{
    private bool Dashboard_Enlarged = false;
    public DashboardView()
    {
        InitializeComponent();
        DataContext = new Dashboard();
    }
    
    private Dashboard ViewModel => DataContext as Dashboard;

    private async void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var window = (Window)this.VisualRoot;

        var dialog = new AddTaskMessage();

        var result = await dialog.ShowDialog<TaskItemViewModel?>(window);

        if (result != null)
        {
            (DataContext as Dashboard)?.AddTask(result);
        }
    }

    private async void SetTaskTimer(object? sender, PointerPressedEventArgs e)
    {
        Console.WriteLine("Pressed");
        if (sender is Button btn &&
            btn.DataContext is TimerViewModel timer &&
            DataContext is Dashboard vm)
        {
            var SenderTask = sender as TaskItemViewModel;
            var window = (Window)this.VisualRoot;

            var dialog = new AddTimerMessage { PrefillTask = SenderTask };
            
            var result = await dialog.ShowDialog<TimerViewModel?>(window);
            
            if (result != null)
            {
                (DataContext as Dashboard)?.AddTimer(result);
            }
        }
    }
    
    private async void AddTimerMethod(object? sender, PointerPressedEventArgs e)
    {
        var window = (Window)this.VisualRoot;

        var dialog = new AddTimerMessage();

        var result = await dialog.ShowDialog<TimerViewModel?>(window);

        if (result != null)
        {
            (DataContext as Dashboard)?.AddTimer(result);
        }
    }

    private void DeleteTask(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn &&
            btn.DataContext is TaskItemViewModel task &&
            DataContext is Dashboard vm)
        {
            vm.RemoveTask(task);
        }
    }
    
    private void DeleteTimer(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn &&
            btn.DataContext is TimerViewModel timer &&
            DataContext is Dashboard vm)
        {
            timer.Stop();
            vm.RemoveTimer(timer);
        }
    }
    
    private async void UndoDelete(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not Dashboard vm)
            return;

        if (!vm.CanUndo) // nothing to undo
        {
            var message = new SimpleMessageDialog("No task to undo");
            await message.ShowDialog((Window)this.VisualRoot!);
            return;
        }

        vm.UndoDelete();
    }

    private void Expand(object? sender, RoutedEventArgs e)
    {
        Dashboard_Enlarged = !Dashboard_Enlarged;

        if (Dashboard_Enlarged)
        {
            ExpandButton.IsVisible = false;
            ShrinkButton.IsVisible = true;
            Grid.SetColumnSpan(TasksPanel, 2);
            Streak.IsVisible = false;
            Timers.IsVisible = false;
        }
        else
        {
            ExpandButton.IsVisible = true;
            ShrinkButton.IsVisible = false;
            Grid.SetColumnSpan(TasksPanel, 1);
            Streak.IsVisible = true;
            Timers.IsVisible = true;
        }
    }
    
    private async void EditSelectedTask(object? sender, RoutedEventArgs e)
    {
        var selectedTask = TASKLISTCONTAINER.SelectedItem as TaskItemViewModel;
        if (selectedTask == null)
        {
            var message = new SimpleMessageDialog("No Task Selected");
            await message.ShowDialog((Window)this.VisualRoot);
            return;
        }

        var window = (Window)this.VisualRoot;

        var dialog = new AddTaskMessage { PrefillTask = selectedTask };
        var result = await dialog.ShowDialog<TaskItemViewModel?>(window);

        if (result != null)
        {
            selectedTask.Title = result.Title;
            selectedTask.Date = result.Date;
            selectedTask.Urgency = result.Urgency;
            selectedTask.IsFinished = result.IsFinished;

            ViewModel?.SaveTasks();
        }
    }

    private void DeleteSelectedTask(object? sender, RoutedEventArgs e)
    {
        var selectedTask = TASKLISTCONTAINER.SelectedItem as TaskItemViewModel;
        if (selectedTask == null)
        {
            var message = new SimpleMessageDialog("No Task Selected");
            message.ShowDialog((Window)this.VisualRoot);
            return;
        }
        (DataContext as Dashboard)?.RemoveTask(selectedTask);
    }

    private void ComboBoxClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.ContextFlyout is MenuFlyout flyout)
        {
            flyout.ShowAt(btn);
        }
    }
}