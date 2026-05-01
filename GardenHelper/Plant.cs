namespace GardenHelper;
public class Plant{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Type{get;set;}
    public Plant(int id, string name, string type){
        Id = id;
        Name = name;
        Type = type;
    }
    public override string ToString(){
        return $"{Name}-{Id}";
    }
}