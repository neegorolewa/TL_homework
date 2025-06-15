using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    private const int Multiplier = 2;

    public string Name { get; }
    public int CurrentHealth { get; private set; }
    public int CurrentArmor { get; private set; }

    public IWeapon Weapon { get; }

    public IRace Race { get; }

    public IArmor Armor { get; }

    public IClass Class { get; }

    public int MaxHealth { get; private set; }
    public int Damage { get; private set; }
    public int MaxArmor { get; private set; }
    public int Initiative { get; private set; }

    public Fighter( string name, IRace race, IClass classFighter, IArmor armor, IWeapon weapon )
    {
        Name = name;
        Race = race;
        Class = classFighter;
        Armor = armor;
        Weapon = weapon;
        MaxHealth = Race.Health + Class.Health;
        Damage = Race.Damage + Weapon.Damage + Class.Damage;
        MaxArmor = Race.Armor + Armor.Armor;
        Initiative = Race.Initiative;
        CurrentHealth = MaxHealth;
        CurrentArmor = MaxArmor;
    }

    public int CalculateDamage()
    {
        Random random = new();

        double damageFactor = random.Next( 80, 121 ) / ( double )100;
        double currentDamage = Damage * damageFactor;

        int criticalDamage = random.Next( 0, 101 );
        if ( criticalDamage > 80 )
        {
            return ( int )currentDamage * Multiplier;
        }

        return ( int )currentDamage;
    }

    public void TakeDamage( int damage )
    {
        int recivedDamage = Math.Max( damage - CurrentArmor, 0 );
        int remainingHealth = CurrentHealth - recivedDamage;
        int lossArmor = damage / 3;
        int remainingArmor = CurrentArmor - lossArmor;

        CurrentHealth = Math.Max( remainingHealth, 0 );
        CurrentArmor = Math.Max( remainingArmor, 0 );
    }
}
