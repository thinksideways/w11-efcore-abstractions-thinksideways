using ConsoleRpgEntities.Models.Attributes;

namespace ConsoleRpgEntities.Models.Characters.Monsters;

public interface IMonster
{
    int Id { get; set; }
    string Name { get; set; }
    void Die();
    void Attack(ITargetable target);
}