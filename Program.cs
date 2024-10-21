using NLog;
using System.Reflection;
using System.Text.Json;

string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");
// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
List<Mario> marios = [];
// check if file exists
if (File.Exists(marioFileName))
{
  marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
  logger.Info($"File deserialized {marioFileName}");
}

// deserializing doinkey kong characters

string dkFileName = "dk.json";
List<DonkeyKongCharacter> dkCharacters = [];
if (File.Exists(dkFileName))
{
  dkCharacters = JsonSerializer.Deserialize<List<DonkeyKongCharacter>>(File.ReadAllText(dkFileName))!;
  logger.Info($"File deserialized {dkFileName}");
}

string sf2FileName = "sf2.json";
List<StreetFighter> sf2Characters = [];
if (File.Exists(sf2FileName))
{
  sf2Characters = JsonSerializer.Deserialize<List<StreetFighter>>(File.ReadAllText(sf2FileName))!;
  logger.Info($"File deserialized {sf2FileName}");
}


do
{
  // display choices to user
  Console.WriteLine("1) Display Mario Characters");
  Console.WriteLine("2) Add Mario Character");
  Console.WriteLine("3) Remove Mario Character");
  Console.WriteLine("4) Display Donkey Kong Characters");
  Console.WriteLine("5) Add Donkey Kong Character");
  Console.WriteLine("6) Remove Donkey Kong Character");
  Console.WriteLine("7) Display Street Fighter Characters");
  Console.WriteLine("8) Add Street Fighter Character");
  Console.WriteLine("9) Remove Street Fighter Character");
  Console.WriteLine("Enter to quit");
  // input selection
  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);
  if (choice == "1")
  {
    // Display Mario Characters
        foreach(var c in marios)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "2")
  {
    // Add Mario Character
        // Generate unique Id
    Mario mario = new()
    {
      Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
    };
        InputCharacter(mario);
        // Add Character
    marios.Add(mario);
    File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
    logger.Info($"Character added: {mario.Name}");
  }
  else if (choice == "3")
  {
    // Remove Mario Character
        Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
            Mario? character = marios.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        marios.Remove(character);
        // serialize list<marioCharacter> into json file
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character Id {Id} removed");
      }
    }
    else if (choice == "4"){
      foreach(var c in dkCharacters)
      {
        Console.WriteLine(c.Display());
      }
    }
    else if (choice == "5"){
      DonkeyKongCharacter donkeyKongCharacter = new();
      {
        Id = dkCharacters.Count == 0 ? 1 : dkCharacters.Max(c => c.Id) + 1
        InputCharacter(donkeyKongCharacter);
        dkCharacters.Add(donkeyKongCharacter);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(dkCharacters));
        logger.Info($"Character added: {donkeyKongCharacter.Name}");
      }
    }
    else if (choice == "6"){
      Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
            DonkeyKongCharacter? character = dkCharacters.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        dkCharacters.Remove(character);
        // serialize list<marioCharacter> into json file
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(dkCharacters));
        logger.Info($"Character Id {Id} removed");
      }
      
    }
    else if (choice == "7"){
      foreach(var c in sf2Characters)
      {
        Console.WriteLine(c.Display());
      }
    }
    else if (choice == "8"){
      StreetFighter sf2Character = new();
      {
        Id = sf2Characters.Count == 0 ? 1 : sf2Characters.Max(c => c.Id) + 1
        InputCharacter(sf2Character);
        sf2Characters.Add(sf2Character);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(sf2Characters));
        logger.Info($"Character added: {sf2Character.Name}");
      }
    }
    else if (choice == "9"){
      Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
            StreetFighter? character = sf2Characters.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        sf2Characters.Remove(character);
        // serialize list<marioCharacter> into json file
        File.WriteAllText(sf2FileName, JsonSerializer.Serialize(sf2Characters));
        logger.Info($"Character Id {Id} removed");
      }
      
    }
    } else {
      logger.Error("Invalid Id");
    }
  
  } else if (string.IsNullOrEmpty(choice)) {
    break;
  } else {
    logger.Info("Invalid choice");
  }
} while (true);

logger.Info("Program ended");

static void InputCharacter(Character character)
{
  Type type = character.GetType();
  PropertyInfo[] properties = type.GetProperties();
  var props = properties.Where(p => p.Name != "Id");
  foreach (PropertyInfo prop in props)
  {
    if (prop.PropertyType == typeof(string))
    {
      Console.WriteLine($"Enter {prop.Name}:");
      prop.SetValue(character, Console.ReadLine());
    } else if (prop.PropertyType == typeof(List<string>)) {
      List<string> list = [];
      do {
        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
        string response = Console.ReadLine()!;
        if (string.IsNullOrEmpty(response)){
          break;
        }
        list.Add(response);
      } while (true);
      prop.SetValue(character, list);
    }
  }
}