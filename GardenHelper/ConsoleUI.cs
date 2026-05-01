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
                        "garden overview",
                        "end"
                    }));

            if(command == "log new plant") {
                LogNewPlant();
            }
            else if(command == "log activity") {
                LogActivity();
            }
            else if(command == "garden overview"){
                ShowGardenOverview();
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

        Plant selectedPlant = SelectPlant();

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
    private Plant SelectPlant(){
        return AnsiConsole.Prompt(new SelectionPrompt<Plant>().Title("Select plant").AddChoices(dataManager.Plants));
    }
    private void ShowGardenOverview(){
        if(dataManager.Plants.Count == 0){
            Console.WriteLine("No plants logged - log a plant first.");
            return;
        }
        string command;
        do{
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>().Title("Garden Overview").AddChoices(new[]{
                    "list plants",
                    "view selected plant",
                    "back"
                }));
            if(command == "list plants"){ShowPlantList();}
            else if(command == "view selected plant"){
                Plant selectedPlant = SelectPlant();
                ShowSelectedPlantOverview(selectedPlant);
            }
        }while(command != "back");
    }
    private void ShowPlantList(){
        Table table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        foreach(Plant plant in dataManager.Plants.OrderBy(plant => plant.Id)){
            table.AddRow(
                plant.Id.ToString(),
                plant.Name,
                plant.Type
            );
        }
        AnsiConsole.Write(table);
    }
    private void ShowSelectedPlantOverview(Plant plant){
    AnsiConsole.MarkupLine($"[bold]Plant:[/] {plant.Name}");
    AnsiConsole.MarkupLine($"[bold]Type:[/] {plant.Type}");
    AnsiConsole.WriteLine();
    List<ActivityLog> logs = dataManager.GetActivityLogsForPlant(plant);
    if(logs.Count == 0){
        Console.WriteLine("No logs found for selected plant.");
        return;
    }
    Table table = new Table();
    table.AddColumn("Date/Time");
    table.AddColumn("Activity");
    table.AddColumn("Comments");
    foreach(ActivityLog log in logs){
        table.AddRow(
            log.Timestamp.ToString("yyyy-MM-dd HH:mm"),
            log.ActivityType.ToString(),
            log.Comments
        );
    }

    AnsiConsole.Write(table);
    }
}