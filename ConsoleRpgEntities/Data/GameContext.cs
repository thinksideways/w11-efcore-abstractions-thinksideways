using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace ConsoleRpgEntities.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Item> Items { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPH for Character hierarchy
            modelBuilder.Entity<Monster>()
                .HasDiscriminator<string>(m=> m.MonsterType)
                .HasValue<Goblin>("Goblin");

            // Configure TPH for Ability hierarchy
            modelBuilder.Entity<Ability>()
                .HasDiscriminator<string>(pa=>pa.AbilityType)
                .HasValue<ShoveAbility>("ShoveAbility");

            // Configure many-to-many relationship
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Abilities)
                .WithMany(a => a.Players)
                .UsingEntity(j => j.ToTable("PlayerAbilities"));

            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Weapon>("Weapon")
                .HasValue<Armour>("Armour");

            modelBuilder.Entity<PlayerItem>().HasNoKey();
            modelBuilder.Entity<PlayerItem>()
                .HasKey(playerItem => new { playerItem.PlayerId, playerItem.ItemId });

            modelBuilder.Entity<PlayerItem>()
                .HasOne(playerItem => playerItem.Player)
                .WithMany(player => player.Items);

            base.OnModelCreating(modelBuilder);
        }

    }
}


