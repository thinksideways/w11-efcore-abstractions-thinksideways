namespace ConsoleRpgEntities.Models.Items;

public class Armour : Item, ICombatGear
{
    public int DefenseRating { get; set; }

    public override void Use() {
        Console.WriteLine($"Your character defends using their {Name}.");
    }
}