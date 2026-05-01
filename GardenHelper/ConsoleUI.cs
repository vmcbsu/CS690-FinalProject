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

        Plant plant = dataManager.AddPlant(plantName);

        Console.WriteLine($"Plant logged: {plant}");
    }

    private void LogActivity() {
        if(dataManager.Plants.Count == 0) {
            Console.WriteLine("No plants ogged yet.");
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

        dataManager.AddActivityLog(selectedPlant, selectedActivity);

        Console.WriteLine($"{selectedActivity} logged for {selectedPlant}.");
    }
}