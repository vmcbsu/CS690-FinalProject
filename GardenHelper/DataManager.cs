namespace GardenHelper;
public class DataManager{
    private string filePath = "garden.txt";
    public List<Plant> Plants{get;set;}
    public List<ActivityLog> ActivityLogs{get;set;}
    private int nextPlantId;
    public DataManager(){
        Plants = new List<Plant>();
        ActivityLogs = new List<ActivityLog>();
        LoadData();
        if(Plants.Count == 0){
            nextPlantId = 1;
        } else {
            nextPlantId = Plants.Max(plant => plant.Id) + 1;
        }
    }
    public Plant AddPlant(string name){
        Plant plant = new Plant(nextPlantId,name);
        nextPlantId++;
        Plants.Add(plant);
        SaveData();
        return plant;
    }
    public void AddActivityLog(Plant plant, ActivityType activityType){
        ActivityLog log = new ActivityLog(plant.Id,activityType);
        ActivityLogs.Add(log);
        SaveData();
    }
    private void SaveData(){
        list<string> lines = new List<string>();
        foreach(Plant plant in Plants){
            lines.Add($"PLANT|{plant.Id}|{Escape(plant.Name)}");
        }
        foreach(ActivityLog log in ActivityLogs){
            lines.Add($"LOG|{log.PlantId}|{log.ActivityType}");
        }
        File.WriteAllLines(filePath,lines);
    }
    
}