using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }

    public int MaxHealth { get; }
    public int MaxArmor { get; }
    public int CurrentHealth { get; }
    public int CurrentArmor { get; }
    public int Damage { get; }
    public int Power { get; }

    public IWeapon Weapon { get; }
    public IRace Race { get; }
    public IArmor Armor { get; }
    public IClass Class { get; }

    public void TakeDamage( int damage );
    public int CalculateDamage();
}
