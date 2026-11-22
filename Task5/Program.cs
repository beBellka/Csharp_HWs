using System;
using System.IO;
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
                "Boots"
            }
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(character, options);

        string filePath = Path.Combine(AppContext.BaseDirectory, "save.json");

        File.WriteAllText(filePath, json);

        string loadedJson = File.ReadAllText(filePath);

        GameCharacter? loadedCharacter =
            JsonSerializer.Deserialize<GameCharacter>(loadedJson);

        if (loadedCharacter != null)
        {
            Console.WriteLine("=== Загруженный персонаж ===");
            Console.WriteLine("Имя: " + loadedCharacter.Name);
            Console.WriteLine("Уровень: " + loadedCharacter.Level);
            Console.WriteLine("Здоровье: " + loadedCharacter.Health);
            Console.WriteLine("Инвентарь: " +
                string.Join(", ", loadedCharacter.Inventory));
        }
    }
}