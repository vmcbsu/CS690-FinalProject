using GardenHelper;
namespace GardenHelper.Tests;
public class ModuleTests{
    [Fact]
    public void Plant_StoresNameTypeAndId(){
        Plant plant = new Plant(1, "durian", "fruit");

        Assert.Equal(1, plant.Id);
        Assert.Equal("durian", plant.Name);
        Assert.Equal("fruit", plant.Type);
        Assert.Equal("durian-1", plant.ToString());
    }

    [Fact]
    public void ActivityLog_StoresActivityData(){
        DateTime timestamp = new DateTime(2026, 5, 1, 9, 30, 0);

        ActivityLog log = new ActivityLog(
            1,
            ActivityType.Watering,
            timestamp,
            "Pests noticed"
        );

        Assert.Equal(1, log.PlantId);
        Assert.Equal(ActivityType.Watering, log.ActivityType);
        Assert.Equal(timestamp, log.Timestamp);
        Assert.Equal("Pests noticed", log.Comments);
    }

    [Fact]
    public void DataManager_AddPlantCreatesPlantAndPlantingLog(){
        string originalDirectory = Directory.GetCurrentDirectory();
        string tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(tempDirectory);

        try{
            Directory.SetCurrentDirectory(tempDirectory);

            DataManager dataManager = new DataManager();

            Plant plant = dataManager.AddPlant("pepper", "vegetable");

            Assert.Equal(1, plant.Id);
            Assert.Single(dataManager.Plants);
            Assert.Single(dataManager.ActivityLogs);
            Assert.Equal(ActivityType.Planting, dataManager.ActivityLogs[0].ActivityType);
            Assert.Equal(plant.Id, dataManager.ActivityLogs[0].PlantId);
        }
        finally{
            Directory.SetCurrentDirectory(originalDirectory);

            if(Directory.Exists(tempDirectory)){
                Directory.Delete(tempDirectory, true);
            }
        }
    }
}