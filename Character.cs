public abstract class Character
{
  public UInt64 Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public virtual string Display()
  {
    return $"Id: {Id}\nName: {Name}\nDescription: {Description}\n";
  }
}

public class DonkeyKongCharacter : Character
{
  public string Species { get; set; }
  
  public override string Display()
  {
    return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {Species}";
  }
}