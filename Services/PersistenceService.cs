using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HaveItMain.Services;
using HaveItMain.ViewModels;

public class PersistenceService
{
    private const string FileName = "tasks.json";

    public void Save(AppState state)
    {
        // 1. Setup the options
        var options = new JsonSerializerOptions { WriteIndented = true };

        // 2. Turn the tasks into a JSON string
        var json = JsonSerializer.Serialize(state.Tasks, options);

        // 3. DEBUG: This will print the JSON to your Rider "Debug" window
        System.Diagnostics.Debug.WriteLine("--- SAVING JSON START ---");
        System.Diagnostics.Debug.WriteLine(json);
        System.Diagnostics.Debug.WriteLine("--- SAVING JSON END ---");

        // 4. Actually save it to the file
        File.WriteAllText(FileName, json);
    }

    public void Load(AppState state)
    {
        if (!File.Exists(FileName))
            return;

        var json = File.ReadAllText(FileName);

        var tasks = JsonSerializer.Deserialize<ObservableCollection<TaskItemViewModel>>(json);

        if (tasks == null)
            return;

        state.Tasks.Clear();

        foreach (var t in tasks)
            state.Tasks.Add(t);
    }
}