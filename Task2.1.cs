using System;

public interface ICombatant
{
    string Name { get; }
    int HP { get; set; }
    bool IsDead { get; }
    int Damage { get; }
    void Attack(ICombatant target);
    void TakeDamage(int amount);
}

public sealed class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 150;
    public int Damage { get; } = 25;
    public bool IsDead => HP <= 0;

    public Knight(string name) => Name = name;

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} не может атаковать: он мёртв.");
            return;
        }
        if (target.IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} не может атаковать мёртвую цель.");
            return;
        }

        Console.WriteLine($"Рыцарь {Name} атакует!");
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public sealed class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public int Damage { get; } = 40;
    public bool IsDead => HP <= 0;

    public Mage(string name) => Name = name;

    public void Attack(ICombatant target)
    {
        if (target.IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
            return;
        }
        if (IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать: он мёртв.");
            return;
        }

        Console.WriteLine($"Маг {Name} применяет двойное заклинание!");
        target.TakeDamage(Damage);
        if (!target.IsDead)
            target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public static class Program
{
    public static void Main()
    {
        ICombatant knight = new Knight("Артур");
        ICombatant mage = new Mage("Мерлин");

        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);
            if (mage.IsDead) break;
            mage.Attack(knight);
        }

        Console.WriteLine(knight.IsDead
            ? $"Победил Маг {mage.Name}!"
            : $"Победил Рыцарь {knight.Name}!");
    }
}
