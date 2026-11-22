№1 А

Name - попадёт в JSON, тк является публичным свойством
Score - попадёт в JSON, тк является публичным свойством
secretCode - не попадёт в JSON, тк это приватное поле
IsAlive - не попадёт в JSON, тк System.Text.Json по умолчанию не сериализует поля, даже если они публичные


№1 Б

Нужно добавить атрибут
[JsonInclude]
private int secretCode = 42;

И подключить пространство имён
using System.Text.Json.Serialization;


№1 В

Поле не попадает в JSON, тк сериализация полей по умолчанию отключена

Способы решения:
1. Заменить публичное поле свойством

public bool IsAlive { get; set; }


2. Добавить настройку IncludeFields

var options = new JsonSerializerOptions
{
    IncludeFields = true
};

string json = JsonSerializer.Serialize(p, options);



№2 А

public class Game
{
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; } = Array.Empty<string>();
    public bool IsMultiplayer { get; set; }
}

Для десериализации:

Game? game = JsonSerializer.Deserialize<Game>(json);


№2 Б

По умолчанию System.Text.Json учитывает регистр букв при сопоставлении имён свойств

Решение:

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Game? game = JsonSerializer.Deserialize<Game>(json, options);


№2 В

Строка является валидным JSON

Её можно десериализовать в массив JsonElement

JsonElement[]? values = JsonSerializer.Deserialize<JsonElement[]>(
    "[1, \"hello\", true, null]");


№3

1. File — File.ReadAllText(), тк небольшой файл нужно прочитать целиком

2. File — File.WriteAllText(), тк данные игрового прогресса можно сохранить целиком одной операцией

3. FileStream — позволяет постепенно дописывать данные в большой файл, не загружая его полностью в память

4. File — File.Exists() проверяет существование файла, File.ReadAllText() читает его содержимое

5. FileStream — позволяет перемещаться по файлу с помощью Seek() и изменять данные в определённой позиции  









