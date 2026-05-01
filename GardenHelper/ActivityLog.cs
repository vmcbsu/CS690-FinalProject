namespace GardenHelper;
public class ActivityLog{
    public int PlantId{get;set;}
    public ActivityType ActivityType{get;set;}
    public ActivityLog(int plantId,ActivityType activityType){
        PlantId = plantId;
        ActivityType = activityType;
    }
}