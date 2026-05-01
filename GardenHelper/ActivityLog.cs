namespace GardenHelper;
public class ActivityLog{
    public int PlantId{get;set;}
    public ActivityType ActivityType{get;set;}
    public DateTime Timestamp{get;set;}
    public string Comments{get;set;}
    public ActivityLog(int plantId,ActivityType activityType,DateTime timestamp,string comments){
        PlantId = plantId;
        ActivityType = activityType;
        Timestamp = timestamp;
        Comments = comments;
    }
}