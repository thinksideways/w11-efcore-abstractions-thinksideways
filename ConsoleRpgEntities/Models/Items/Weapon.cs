namespace ConsoleRpgEntities.Models.Items;

public class Weapon : Item, ICombatGear
{
    public int AttackPower { get; set; }

    public override void Use() {
        Console.WriteLine($"Your character attacks using their {Name}.");
    }
}