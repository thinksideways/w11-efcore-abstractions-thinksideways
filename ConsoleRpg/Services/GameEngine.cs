using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly OutputManager _outputManager;

    private IPlayer _player;
    private IMonster _goblin;

    public GameEngine(GameContext context, MenuManager menuManager, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop()
    {
        _outputManager.Clear();

        while (true)
        {
            _outputManager.WriteLine("Choose an action:", ConsoleColor.Cyan);
            _outputManager.WriteLine("1. Attack");
            _outputManager.WriteLine("2. Quit");
            _outputManager.WriteLine("3. Burglarize the blacksmith");

            _outputManager.Display();

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AttackCharacter();
                    break;
                case "2":
                    _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                    _outputManager.Display();
                    Environment.Exit(0);
                    break;
                case "3":
                    RobBlacksmith();
                    break;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 1.", ConsoleColor.Red);
                    break;
            }
        }
    }

    private void AttackCharacter()
    {
        if (_goblin is ITargetable targetableGoblin)
        {
            _player.Attack(targetableGoblin);
            // var playerDamage = _player.Items.Sum(player => player.Item.AttackPower);
            // if (_player.Items.Contains(aWeapon => aWeapon.AttackPower > targetableGobllin))
            _player.UseAbility(_player.Abilities.First(), targetableGoblin);
        }
    }

    private void RobBlacksmith()
    {
        Console.WriteLine("You see weapons on one shelf and armour on the other.");

        bool choosingGearType = true;
        do
        {
            Console.WriteLine("1. Loot Weapons.");
            Console.WriteLine("2. Loot Armour.");
            Console.WriteLine("3. Nevermind.");

            var response = Console.ReadLine();
            switch (response)
            {
                case "1": ChooseWeapons(); return;
                case "2": ChooseArmour(); return;
                case "3": choosingGearType = false; break;
                default: Console.WriteLine("Can you read?"); break;
            }
        } while (choosingGearType);
    }

    public void ChooseWeapons()
    {
        var allWeapons = _context.Items.OfType<Weapon>();
        Console.WriteLine("Select a weapon to equip: ");

        foreach (var weapon in allWeapons)
        {
            Console.WriteLine($"{weapon.Id}: {weapon.Name} - Durability: {weapon.Durability} - Attack Rating: {weapon.AttackPower}");
        }

        var response = Console.ReadLine();
        int weaponId = 0;
        int.TryParse(response, out weaponId);

        var items = _player.Items;
        if (weaponId > 0)
        {
            var foundWeapon = _context.Items.Where(item => item.Id.Equals(weaponId)).ToList();
            if (foundWeapon.Count() > 0)
            {
                _player.Items.Add(new PlayerItem { PlayerId =_player.Id, ItemId = foundWeapon.First().Id});
                _context.SaveChanges();
                Console.WriteLine($"Your character has chosen a {foundWeapon.First().Name}");
            }
        }
    }

    public void ChooseArmour()
    {
        var allArmour = _context.Items.OfType<Armour>().ToList();
        Console.WriteLine("Select a piece of armour to equip: ");
        foreach (var piece in allArmour)
        {
            Console.WriteLine($"{piece.Id}: {piece.Name} - Durability: {piece.Durability} - Defense Rating: {piece.DefenseRating}");
        }

        var response = Console.ReadLine();
        int armourId = 0;
        int.TryParse(response, out armourId);

        var items = _player.Items;
        if (armourId > 0)
        {
            var foundArmour = _context.Items.Where(item => item.Id.Equals(armourId)).ToList();
            if (foundArmour.Count() > 0)
            {
                _player.Items.Add(new PlayerItem { PlayerId =_player.Id, ItemId = foundArmour.First().Id});
                _context.SaveChanges();
                Console.WriteLine($"Your character has chosen a {foundArmour.First().Name}");
            }
        }
    }

    private void SetupGame()
    {
        _player = _context.Players.OfType<Player>().FirstOrDefault();
        _outputManager.WriteLine($"{_player.Name} has entered the game.", ConsoleColor.Green);

        // Load monsters into random rooms 
        LoadMonsters();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }

    private void LoadMonsters()
    {
        _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();
    }

}
