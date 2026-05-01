namespace GardenHelper;
public class Plant{
    public int Id{get;set;}
    public string Name{get;set;}
    public Plant(int id, string name){
        Id = id;
        Name = name;
    }
    public override string ToString(){
        return $"{Name}-{Id}";
    }
}