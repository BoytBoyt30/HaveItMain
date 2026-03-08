using System;
using ReactiveUI;

namespace HaveItMain.ViewModels;

public class TaskItemViewModel : ViewModelBase
{
    private string _title;
    private DateTime _date;
    private string _urgency;
    private bool _isFinished;
    
    private DateTime? _completedDate;
    public DateTime? CompletedDate 
    {
        get => _completedDate;
        set => this.RaiseAndSetIfChanged(ref _completedDate, value);
    }
    
    public bool IsUrgent => Urgency == "Urgent";
    public bool IsPending => Urgency == "Pending";
    public bool IsNotUrgent => Urgency == "Not Urgent";

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public DateTime Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }
    
    

    public string Urgency
    {
        get => _urgency;
        set 
        {
            this.RaiseAndSetIfChanged(ref _urgency, value);
            this.RaisePropertyChanged(nameof(IsUrgent));
            this.RaisePropertyChanged(nameof(IsPending));
            this.RaisePropertyChanged(nameof(IsNotUrgent));
        }
    }
    
    public void ToggleStatus(bool isDone)
    {
        // 1. Set the raw values
        IsFinished = isDone;
    
        // 2. Explicitly stamp the date
        if (isDone)
        {
            CompletedDate = DateTime.Today;
            System.Diagnostics.Debug.WriteLine($"[METHOD] Stamped {Title} at {CompletedDate}");
        }
        else
        {
            CompletedDate = null;
            System.Diagnostics.Debug.WriteLine($"[METHOD] Cleared {Title}");
        }
    }

    public DateTime date_created { get; }  // keep read-only

    public bool IsFinished
    {
        get => _isFinished;
        set 
        {
            // 1. If the value isn't actually changing, stop here.
            if (_isFinished == value) return;

            // 2. Update the backing field and tell the UI
            this.RaiseAndSetIfChanged(ref _isFinished, value);

            // 3. Logic Trigger: Only stamp the date if it's currently null
            // This prevents overwriting an old "Yesterday" date with "Today"
            if (value && CompletedDate == null)
            {
                CompletedDate = DateTime.Today;
            }
            else if (!value)
            {
                CompletedDate = null;
            }
        
            System.Diagnostics.Debug.WriteLine($"Task '{Title}' toggled. Done: {value}, Stamp: {CompletedDate}");
        }
    }

    public TaskItemViewModel(string title, DateTime date, DateTime date_created, string urgency, bool isFinished = false, DateTime? completedDate = null)
    {
        _title = title;
        _date = date;
        _urgency = urgency;
        _isFinished = isFinished;
        this.date_created = date_created;
        this.CompletedDate = completedDate;
    }
    
    public override string ToString()
    {
        return $"{Title} | Due: {Date:yyyy-MM-dd} | Created: {date_created:yyyy-MM-dd} | Urgency: {Urgency} | Done: {IsFinished}";
    }
}