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
    private void LoadData(){
        if(!File.Exists(filePath)){
            return;
        }
        string[] lines = File.ReadAllLines(filePath);
        foreach(string line in lines){
            string[] parts = line.Split('|');
            if(parts.Length == 0){
                continue;
            }
            if(parts[0] == "PLANT" && parts.Length >= 3){
                int id = int.Parse(parts[1]);
                string name = Unescape(parts[2]);
                Plants.Add(new Plant(id, name));
            }
            else if(parts[0] == "LOG" && parts.Length >= 3){
                int plantId = int.Parse(parts[1]);
                ActivityType activityType = Enum.Parse<ActivityType>(parts[2]);
                ActivityLogs.Add(new ActivityLog(plantId, activityType));
            }
        }
    }
    private string Escape(string value){
        return value.Replace("|","/");
    }
    private string Unescape(string value){
        return value;
    }
    
}