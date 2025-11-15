using ConsoleRpgEntities.Models.Characters;

namespace ConsoleRpgEntities.Models.Items;

public class PlayerItem
{
    public int PlayerId { get; set; }
    public virtual Player Player { get; set; }
    public int ItemId { get; set; }
    public virtual Item item { get; set; }
}