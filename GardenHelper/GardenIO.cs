namespace GardenHelper;
public class GardenIO{
    private string filePath = "garden.txt";
    public void Save(List<Plant> plants, List<ActivityLog> activityLogs){
        List<string> lines = new List<string>();
        foreach(Plant plant in plants){
            lines.Add($"PLANT|{plant.Id}|{Escape(plant.Name)}|{Escape(plant.Type)}");
        }
        foreach(ActivityLog log in activityLogs){
            lines.Add($"LOG|{log.PlantId}|{log.ActivityType}|{log.Timestamp:O}|{Escape(log.Comments)}");
        }
        File.WriteAllLines(filePath, lines);
    }
    public void Load(List<Plant> plants, List<ActivityLog> activityLogs){
        if(!File.Exists(filePath)){
            return;
        }
        string[] lines = File.ReadAllLines(filePath);
        foreach(string line in lines){
            string[] parts = line.Split('|');
            if(parts.Length == 0){
                continue;
            }
            if(parts[0] == "PLANT" && parts.Length >=3){
                int id = int.Parse(parts[1]);
                string name = Unescape(parts[2]);
                string type = "";
                if(parts.Length >= 4){
                    type = Unescape(parts[3]);
                }
                plants.Add(new Plant(id, name, type));
            }
            else if(parts[0] == "LOG" && parts.Length >= 3){
                int plantId = int.Parse(parts[1]);
                ActivityType activityType = Enum.Parse<ActivityType>(parts[2]);
                DateTime timestamp = DateTime.Now;
                string comments = "";
                if(parts.Length >= 4){
                    timestamp = DateTime.Parse(parts[3]);
                }
                if(parts.Length >= 5){
                    comments = Unescape(parts[4]);
                }
                activityLogs.Add(new ActivityLog(plantId, activityType, timestamp, comments));
            }
        }
    }
    private string Escape(string value){
        return value.Replace("|", "/");
    }
    private string Unescape(string value){
        return value;
    }
}