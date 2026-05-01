namespace GardenHelper;
public class DataManager{
    private GardenIO gardenIO;
    public List<Plant> Plants{get;set;}
    public List<ActivityLog> ActivityLogs{get;set;}
    private int nextPlantId;
    public DataManager(){
        gardenIO = new GardenIO();
        Plants = new List<Plant>();
        ActivityLogs = new List<ActivityLog>();
        gardenIO.Load(Plants,ActivityLogs);
        if(Plants.Count == 0){
            nextPlantId = 1;
        } else {
            nextPlantId = Plants.Max(plant => plant.Id) + 1;
        }
    }
    public Plant AddPlant(string name, string type){
        Plant plant = new Plant(nextPlantId,name,type);
        nextPlantId++;
        Plants.Add(plant);
        AddActivityLogWithoutSaving(plant,ActivityType.Planting,"New plant added.");
        SaveData();
        return plant;
    }
    public void AddActivityLog(Plant plant, ActivityType activityType, string comments){
        AddActivityLogWithoutSaving(plant,activityType,comments);
        SaveData();
    }
    private void AddActivityLogWithoutSaving(Plant plant, ActivityType activityType,string comments){
        ActivityLog log = new ActivityLog(plant.Id,activityType,DateTime.Now,comments);
        ActivityLogs.Add(log);
    }
    private void SaveData(){
        gardenIO.Save(Plants,ActivityLogs);
    }
}