using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public sealed class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor) =>
        Math.Max(1, baseDamage - targetArmor / 2);
}

public sealed class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);
        return targetArmor == 0 ? damage * 2 : damage;
    }
}

public sealed class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);
        if (baseDamage > 50) damage += 10;
        return damage;
    }
}

public sealed class Unit
{
    public string Name { get; }
    public int HP { get; set; }
    public int Armor { get; }

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = Math.Max(0, hp);
        Armor = Math.Max(0, armor);
    }
}

public sealed class DamageSystem
{
    private readonly IDamageCalculator _calculator;

    public DamageSystem(IDamageCalculator calculator) =>
        _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = _calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}

public static class Program
{
    public static void Main()
    {
        IDamageCalculator[] calculators =
        {
            new PhysicalDamageCalculator(),
            new MagicDamageCalculator(),
            new FireDamageCalculator()
        };
        string[] damageTypes = { "Физический", "Магический", "Огненный" };

        for (int i = 0; i < calculators.Length; i++)
        {
            var target = new Unit("Страж", hp: 120, armor: 20);
            var system = new DamageSystem(calculators[i]);
            Console.Write($"{damageTypes[i]} урон: ");
            system.ApplyDamage(target, baseDamage: 60);
        }
    }
}

//Чтоб добавить новый тип урона нужно создать класс IceDamageCalculator, реализовать в нём
// интерфейс IDamageCalculator и прописать свою формулу урона. Потом передать его в DamageSystem