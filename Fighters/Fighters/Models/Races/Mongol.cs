namespace Fighters.Models.Races;

public class Mongol : IRace
{
    public int Damage { get; } = 30;
    public int Health { get; } = 150;
    public int Armor { get; } = 20;
    public int Power { get; } = 70;
}
