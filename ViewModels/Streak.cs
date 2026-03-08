using System;
using System.Collections.ObjectModel;
using HaveItMain.Services;
using ReactiveUI;

namespace HaveItMain.ViewModels;

public class Streak : ViewModelBase, IHasTitle
{
    public string Title => "STREAK";
    public AppState State => _state;
    public readonly AppState _state;
    private readonly StreakPersistenceService _persistence;

    // --- COMPUTED PROPERTIES (The "Windows" into your State) ---
    public bool StreakStarted => _state.StreakStarted;
    
    // Fix: Point these to the actual data in the state
    public string FormattedStartDate => _state.CurrentStreak?.StartDate.ToString("MMMM dd, yyyy") ?? "No Start Date";
    public bool IsTodayCompleted => _state.CurrentStreak?.IsTodayDone ?? false;
    public int CurrentTally => _state.CurrentStreak?.CompletedDaysCount ?? 0;
    
    public bool Is7DayStreak => _state.CurrentStreak?.DurationDays == 7;
    public bool Is14DayStreak => _state.CurrentStreak?.DurationDays == 14;

    public Streak(AppState state)
    {
        _state = state;
        _persistence = new StreakPersistenceService();

        // Watch BOTH the streak object and the started boolean
        _state.WhenAnyValue(x => x.CurrentStreak, x => x.StreakStarted)
            .Subscribe(_ => {
                // Use the EXACT names of the public properties in THIS class
                this.RaisePropertyChanged(nameof(State));
                this.RaisePropertyChanged(nameof(StreakStarted));
                this.RaisePropertyChanged(nameof(Is7DayStreak));
                this.RaisePropertyChanged(nameof(Is14DayStreak));
            });
    }
    
    
    public void StartNewStreak()
    {
        var newStreak = new Services.STREAK 
        { 
            StartDate = DateTime.Today, 
            DurationDays = 7,
            // PASS THE SERVICE HERE
            NotificationService = _state.NotificationService 
        };
        newStreak.GenerateDays();
    
        _state.CurrentStreak = newStreak;
        _state.StreakStarted = true;
        _persistence.Save(newStreak);
        
        Console.WriteLine("New streak created and saved to streak.json!");
    }
    
    public void StartNewStreakFourteen()
    {
        var newStreak = new Services.STREAK 
        { 
            StartDate = DateTime.Today, 
            DurationDays = 14
        };
        newStreak.GenerateDays();
        
        _state.CurrentStreak = newStreak;
        _state.StreakStarted = true;
        
        _persistence.Save(newStreak); 
        
        Console.WriteLine("New streak created and saved to streak.json!");
    }
}