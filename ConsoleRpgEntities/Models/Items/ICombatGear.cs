
namespace ConsoleRpgEntities.Models.Items;

public interface ICombatGear
{
    // Decided not to pursue this any farther for week 11 as it wasn't required and I don't want to have to rework more code for the final
    string Name { get; set; }
    int Durability { get; set; }
}