using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;

namespace ConsoleRpgEntities.Models.Items;

public abstract class Item
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Durability { get; set; }

    public virtual void Use() {
        Console.WriteLine($"Your character looks at their {Name}");
    }
}