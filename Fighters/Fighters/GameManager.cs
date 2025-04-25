using Fighters.Models.Fighters;

namespace Fighters;

public class GameManager
{
    private const int Dead = 0;

    public IFighter PlayAndGetWinner( List<IFighter> fighters )
    {
        List<IFighter> aliveFighters = fighters.OrderByDescending(
            f => f.Power
        ).ToList();

        int countFighters = aliveFighters.Count();
        int round = 0;

        while ( true )
        {
            for ( int i = 0; i < aliveFighters.Count; i++ )
            {
                Console.WriteLine( $"Раунд {round++}" );
                Console.WriteLine( $"Осталось бойцов: {aliveFighters.Count}" );

                int attackerNumber = i;
                int defenderNumber = ( i + 1 ) % countFighters;

                IFighter attacker = aliveFighters[ attackerNumber ];
                IFighter defender = aliveFighters[ defenderNumber ];

                Console.WriteLine( $"Бой {attacker.Name} vs {defender.Name}" );
                if ( FightAndCheckIfOpponentDead( attacker, defender ) )
                {
                    Console.WriteLine( $"Боец {defender.Name} убит!" );
                    aliveFighters.Remove( defender );
                    countFighters--;
                    if ( aliveFighters.Count == 1 )
                    {
                        return attacker;
                    }
                }
                Console.WriteLine();
            }
        }
    }

    private bool FightAndCheckIfOpponentDead( IFighter roundOwner, IFighter opponent )
    {
        int damage = roundOwner.CalculateDamage();
        opponent.TakeDamage( damage );

        Console.WriteLine(
            $"""
            Боец {opponent.Name} получает {damage} урона.
            Уровень здоровья: {opponent.CurrentHealth}, Уровень брони: {opponent.CurrentArmor}
            """
            );

        return opponent.CurrentHealth == Dead;
    }
}
