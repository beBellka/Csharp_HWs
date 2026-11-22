using System;
using System.Text.Json;

public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}

class Program
{
    static void Main()
    {
        GameCharacter character = new GameCharacter
        {
            Name = "Bob",
            Level = 67,
            Health = 100,
            Inventory = new string[]
            {
                "Sword",
                "Apple",
                "Galoshes"
            }
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(character, options);

        Console.WriteLine(json);

        GameCharacter? restoredCharacter =
            JsonSerializer.Deserialize<GameCharacter>(json);

        if (restoredCharacter != null)
        {
            Console.WriteLine("Имя: " + restoredCharacter.Name);
            Console.WriteLine("Количество предметов: " +
                restoredCharacter.Inventory.Length);
        }
    }
}