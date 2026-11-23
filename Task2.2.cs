using System;
using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public sealed class Player
{
    public string Name { get; }
    public int HP { get; private set; }
    public int Damage { get; private set; }

    public Player(string name, int hp, int damage)
    {
        Name = name;
        HP = Math.Max(0, hp);
        Damage = Math.Max(0, damage);
    }

    public void Heal(int amount)
    {
        if (amount > 0) HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        if (amount > 0) Damage += amount;
    }

    public void TakeDamage(int amount)
    {
        if (amount > 0) HP = Math.Max(0, HP - amount);
    }

    public bool IsDead => HP <= 0;
}

public sealed class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP.";
    public Rarity Rarity => Rarity.Common;
    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine($"Вы выпили зелье и восстановили 50 HP. HP игрока: {player.HP}");
    }
}

public sealed class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10.";
    public Rarity Rarity => Rarity.Rare;
    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine($"Вы взяли меч. Урон увеличен на 10 и теперь равен {player.Damage}.");
    }
}

public sealed class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Проклинает игрока, нанося 20 урона, и увеличивает урон на 30.";
    public Rarity Rarity => Rarity.Legendary;
    public void Use(Player player)
    {
        player.TakeDamage(20);
        player.IncreaseDamage(30);
        Console.WriteLine($"Свиток проклял игрока: -20 HP, +30 к урону. HP: {player.HP}, урон: {player.Damage}.");
    }
}

public static class Program
{
    public static void Main()
    {
        var player = new Player("Алекс", hp: 70, damage: 15);
        List<IUsableItem> items = new()
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            Console.WriteLine($"Предмет: {item.Name} ({item.Rarity}) — {item.Description}");
            item.Use(player);
        }

        Console.WriteLine($"Итог: {player.Name}; HP: {player.HP}; урон: {player.Damage}; состояние: {(player.IsDead ? "мёртв" : "жив")}.");
    }
}
