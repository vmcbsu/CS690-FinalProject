namespace GardenHelper;

using Spectre.Console;

public class ConsoleUI {
    DataManager dataManager;

    public ConsoleUI() {
        dataManager = new DataManager();
    }

    public void Show() {
        string command;

        do {
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Garden Helper")
                    .AddChoices(new[] {
                        "log new plant",
                        "log activity",
                        "end"
                    }));

            if(command == "log new plant") {
                LogNewPlant();
            }
            else if(command == "log activity") {
                LogActivity();
            }

        } while(command != "end");
    }

    private void LogNewPlant() {
        string plantName = AnsiConsole.Prompt(
            new TextPrompt<string>("Plant name:"));
        string plantType = AnsiConsole.Prompt(
            new TextPrompt<string>("Plant type:"));

        Plant plant = dataManager.AddPlant(plantName,plantType);

        Console.WriteLine($"Plant logged: {plant}");
    }

    private void LogActivity() {
        if(dataManager.Plants.Count == 0) {
            Console.WriteLine("No plants logged yet.");
            Console.WriteLine("Log a new plant first.");
            return;
        }

        Plant selectedPlant = AnsiConsole.Prompt(
            new SelectionPrompt<Plant>()
                .Title("Select a plant")
                .AddChoices(dataManager.Plants));

        ActivityType selectedActivity = AnsiConsole.Prompt(
            new SelectionPrompt<ActivityType>()
                .Title("Select an activity to log")
                .AddChoices(new[] {
                    ActivityType.Watering,
                    ActivityType.Pruning,
                    ActivityType.Change
                }));

        string comments;
        if(selectedActivity == ActivityType.Change){
            comments = AnsiConsole.Prompt(new TextPrompt<string>("Description of change:"));}
            else{
                comments = AnsiConsole.Prompt(new TextPrompt<string>("Comments (optional):").AllowEmpty());
            
        }
        dataManager.AddActivityLog(selectedPlant, selectedActivity,comments);

        Console.WriteLine($"{selectedActivity} logged for {selectedPlant}.");
    }
}