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

public class StreetFighter : Character
{
  public List<string> SpecialMoves { get; set; } = new List<string>();

    public override string Display()
    {
      string moves = string.Join(", ", SpecialMoves);
      return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecial Moves: {string.Join(", ", SpecialMoves)}\n";
    }
}