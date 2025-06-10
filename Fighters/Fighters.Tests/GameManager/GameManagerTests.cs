using Fighters.Models.Fighters;
using Fighters.Tests.ConsoleMethods;
using Moq;
using Moq.Language;

namespace Fighters.Tests.GameManager;

public class GameManagerTests
{
    private readonly Mock<Random> _randomMock = new();

    [Fact]
    public void PlayAndGetWinner_TwoFightersWhenOneFighterStronger_ReturnsLoserWithZeroHealthAndFirstAttackedAttacker()
    {
        //Arrange
        SetupNextRandomValues( [ 100, 50 ] );
        Mock<IFighter> attackerFighter = CreateFighterMock( "Attacker", 1000, 200, 100, 10, _randomMock.Object );
        Mock<IFighter> defenderFighter = CreateFighterMock( "Defender", 10, 10, 10, 1, _randomMock.Object );
        List<IFighter> fighters = [ attackerFighter.Object, defenderFighter.Object ];

        //Act
        IFighter winner = Fighters.GameManager.PlayAndGetWinner( fighters );

        //Assert
        Assert.NotNull( winner );
        Assert.Equal( "Attacker", winner.Name );
        Assert.True( defenderFighter.Object.CurrentHealth == 0 );
    }

    [Fact]
    public void PlayAndGetWinner_ThreeFightersWhenOneFighterIsStronger_StrongerFighterWinsAndOtherAreDead()
    {
        //Arrange
        SetupNextRandomValues( [ 100, 50, 50 ] );
        Mock<IFighter> firstFighter = CreateFighterMock( "first", 1000, 200, 100, 10, _randomMock.Object );
        Mock<IFighter> secondFighter = CreateFighterMock( "second", 10, 10, 10, 5, _randomMock.Object );
        Mock<IFighter> thirdFighter = CreateFighterMock( "third", 20, 20, 20, 1, _randomMock.Object );
        List<IFighter> fighters = [ firstFighter.Object, secondFighter.Object, thirdFighter.Object ];

        //Act
        IFighter winner = Fighters.GameManager.PlayAndGetWinner( fighters );

        //Assert
        Assert.NotNull( winner );
        Assert.Equal( firstFighter.Object.Name, winner.Name );
        Assert.True( secondFighter.Object.CurrentHealth == 0 );
        Assert.True( thirdFighter.Object.CurrentHealth == 0 );
    }

    [Fact]
    public void PlayAndGetWinner_TwoFighters_ReturnsRightOutMessages()
    {
        //Arrange
        using ConsoleFixture consoleFixture = new();
        consoleFixture.StringWriter.GetStringBuilder().Clear();
        SetupNextRandomValues( [ 100, 50 ] );
        Mock<IFighter> attackerFighter = CreateFighterMock( "Attacker", 1000, 200, 100, 10, _randomMock.Object );
        Mock<IFighter> defenderFighter = CreateFighterMock( "Defender", 10, 10, 0, 1, _randomMock.Object );
        List<IFighter> fighters = [ attackerFighter.Object, defenderFighter.Object ];

        //Act
        IFighter winner = Fighters.GameManager.PlayAndGetWinner( fighters );

        //Assert
        string output = consoleFixture.StringWriter.ToString();
        Assert.Contains( "Раунд 0", output );
        Assert.Contains( "Осталось бойцов: 2", output );
        Assert.Contains(
            $"""
            Бой {attackerFighter.Object.Name} vs {defenderFighter.Object.Name}
            Боец {defenderFighter.Object.Name} получает 200 урона.
            Уровень здоровья: {defenderFighter.Object.CurrentHealth}, Уровень брони: {defenderFighter.Object.CurrentArmor}
            Боец {defenderFighter.Object.Name} убит!
            """, output );

    }

    private static Mock<IFighter> CreateFighterMock( string name, int health, int damage, int armor, int initiative, Random random )
    {
        Mock<IFighter> fighterMock = new();

        int currentHealth = health;
        int currentArmor = armor;

        fighterMock.Setup( f => f.Name ).Returns( name );
        fighterMock.Setup( f => f.CurrentHealth ).Returns( () => currentHealth );
        fighterMock.Setup( f => f.MaxHealth ).Returns( health );
        fighterMock.Setup( f => f.Damage ).Returns( damage );
        fighterMock.Setup( f => f.CurrentArmor ).Returns( () => currentArmor );
        fighterMock.Setup( f => f.MaxArmor ).Returns( armor );
        fighterMock.Setup( f => f.Initiative ).Returns( initiative );

        fighterMock.Setup( f => f.CalculateDamage() ).Returns( () =>
        {
            double damageFactor = random.Next( 80, 121 ) / 100.0;
            double currentDamage = damage * damageFactor;

            return random.Next( 0, 101 ) > 80
                ? ( int )currentDamage * 2
                : ( int )currentDamage;
        } );

        fighterMock.Setup( f => f.TakeDamage( It.IsAny<int>() ) )
                .Callback<int>( dmg =>
                {
                    int receivedDamage = Math.Max( dmg - currentArmor, 0 );
                    currentHealth = Math.Max( currentHealth - receivedDamage, 0 );
                    currentArmor = Math.Max( currentArmor - ( dmg / 3 ), 0 );
                } );

        return fighterMock;
    }

    private void SetupNextRandomValues( int[] values )
    {
        ISetupSequentialResult<int> sequence = _randomMock.SetupSequence( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) );
        foreach ( var value in values )
        {
            sequence.Returns( value );
        }

        sequence.Returns( values.Last() );
    }
}
