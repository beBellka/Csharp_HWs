using System;
using System.IO;
using System.Text.Json;

public class GameSettings
{
    public string PlayerName { get; set; } = "";
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "settings.json"
        );

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        GameSettings settings;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            settings = JsonSerializer.Deserialize<GameSettings>(json)
                       ?? new GameSettings();

            ShowSettings(settings);
        }
        else
        {
            Console.WriteLine(
                "Файл настроек не найден. Создаём новые настройки."
            );

            settings = new GameSettings();

            Console.Write("Введите имя игрока: ");
            settings.PlayerName = Console.ReadLine() ?? "";

            Console.WriteLine("1 — Легко");
            Console.WriteLine("2 — Нормально");
            Console.WriteLine("3 — Сложно");
            Console.Write("Введите сложность: ");
            settings.Difficulty = int.Parse(Console.ReadLine()!);

            Console.Write("Введите громкость музыки (0–100): ");
            settings.MusicVolume = int.Parse(Console.ReadLine()!);

            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(filePath, json);

            ShowSettings(settings);
        }
    }

    static void ShowSettings(GameSettings settings)
    {
        Console.WriteLine("\n=== Текущие настройки ===");
        Console.WriteLine("Имя игрока: " + settings.PlayerName);

        string difficulty = settings.Difficulty switch
        {
            1 => "Легко",
            2 => "Нормально",
            3 => "Сложно",
            _ => "Неизвестно"
        };

        Console.WriteLine("Сложность: " + difficulty);
        Console.WriteLine("Громкость музыки: " + settings.MusicVolume);
    }
}