using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;

namespace HaveItMain.ViewModels;

public class Timer : ViewModelBase, IHasTitle
{
    public string Title => "TIMER";

    public ObservableCollection<TimerViewModel> Timers { get; }
    private readonly ObservableAsPropertyHelper<bool> _hasNoTimers;
    public bool HasNoTimers => _hasNoTimers.Value;

    public Timer(ObservableCollection<TimerViewModel> sharedTimers)
    {
        Timers = sharedTimers;

        _hasNoTimers = this.WhenAnyValue(x => x.Timers.Count)
            .Select(count => count == 0)
            .ToProperty(this, x => x.HasNoTimers);

        foreach (var t in Timers)
            t.Start();
    }
    
    public void AddTimer(TimerViewModel timer)
    {
        timer.Start();
        Timers.Add(timer);
    }
}