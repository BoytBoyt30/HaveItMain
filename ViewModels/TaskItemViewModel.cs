using System;

namespace HaveItMain.ViewModels;

public class TaskItemViewModel : ViewModelBase
{
    public string Title { get; }
    public DateTime Date { get; }
    public string Urgency { get; }
    public bool isFinished { get; }

    public TaskItemViewModel(string title, DateTime date, string urgency, bool isFinished = false)
    {
        Title = title;
        Date =  date;
        Urgency = urgency.ToLower();
        this.isFinished = isFinished;
    }
}