using Fighters.Models.Fighters;

namespace Fighters;

public static class GameManager
{
    private const int DeadFighterHealth = 0;

    public static IFighter PlayAndGetWinner( List<IFighter> fighters )
    {
        List<IFighter> aliveFighters = fighters.OrderByDescending(
            f => f.Initiative
        ).ToList();

        int round = 0;

        while ( aliveFighters.Count() > 1 )
        {
            for ( int i = 0; i < aliveFighters.Count(); i++ )
            {
                Console.WriteLine( $"Раунд {round++}" );
                Console.WriteLine( $"Осталось бойцов: {aliveFighters.Count}" );

                int attackerNumber = i;
                int defenderNumber = ( i + 1 ) % aliveFighters.Count();

                IFighter attacker = aliveFighters[ attackerNumber ];
                IFighter defender = aliveFighters[ defenderNumber ];

                Console.WriteLine( $"Бой {attacker.Name} vs {defender.Name}" );

                if ( FightAndCheckIfOpponentDead( attacker, defender ) )
                {
                    Console.WriteLine( $"Боец {defender.Name} убит!" );
                    aliveFighters.Remove( defender );

                    break;
                }
                Console.WriteLine();
            }
        }

        return aliveFighters[ 0 ];
    }

    private static bool FightAndCheckIfOpponentDead( IFighter roundOwner, IFighter opponent )
    {
        int damage = roundOwner.CalculateDamage();
        opponent.TakeDamage( damage );

        Console.WriteLine(
            $"""
            Боец {opponent.Name} получает {damage} урона.
            Уровень здоровья: {opponent.CurrentHealth}, Уровень брони: {opponent.CurrentArmor}
            """
            );

        return opponent.CurrentHealth == DeadFighterHealth;
    }
}
