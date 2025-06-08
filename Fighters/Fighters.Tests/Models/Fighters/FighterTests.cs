using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.Tests.Models.Fighters;

public class FighterTests
{
    private readonly Mock<IArmor> _armorMock;
    private readonly Mock<IClass> _classMock;
    private readonly Mock<IRace> _raceMock;
    private readonly Mock<IWeapon> _weaponMock;

    private readonly Fighter _fighter;

    public FighterTests()
    {
        _raceMock = new();
        _weaponMock = new();
        _armorMock = new();
        _classMock = new();

        _armorMock.Setup( a => a.Armor ).Returns( 10 );

        _classMock.Setup( c => c.Health ).Returns( 50 );
        _classMock.Setup( c => c.Damage ).Returns( 5 );

        _raceMock.Setup( r => r.Damage ).Returns( 10 );
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _raceMock.Setup( r => r.Armor ).Returns( 20 );
        _raceMock.Setup( r => r.Initiative ).Returns( 40 );

        _weaponMock.Setup( w => w.Damage ).Returns( 20 );

        _fighter = new Fighter(
            "Fighter1",
            _raceMock.Object,
            _classMock.Object,
            _armorMock.Object,
            _weaponMock.Object );

    }

    [Fact]
    public void Constructor_ValidParameters_InitialisationPropertiesCorrect()
    {
        //Assert
        Assert.Equal( "Fighter1", _fighter.Name );
        Assert.Equal( 150, _fighter.MaxHealth );
        Assert.Equal( 35, _fighter.Damage );
        Assert.Equal( 30, _fighter.MaxArmor );
        Assert.Equal( 40, _fighter.Initiative );
        Assert.Equal( 150, _fighter.CurrentHealth );
        Assert.Equal( 30, _fighter.CurrentArmor );
    }

    [Fact]
    public void CalculateDamage_NormalConditions_ReturnsDamageInRange()
    {
        //Arrange
        const int minExpectedDamage = ( int )( 0.8 * 35 );
        const int maxExpectedDamage = ( int )( 1.2 * 35 * 2 );

        //Act
        int damage = _fighter.CalculateDamage();

        //Assert
        Assert.InRange( damage, minExpectedDamage, maxExpectedDamage );

    }

    [Theory]
    [InlineData( 200, 0, 0 )]
    [InlineData( 100, 0, 80 )]
    [InlineData( 0, 30, 150 )]
    public void TakeDamage_VariosDamageValues_ClaculateCorrectReaminigHealthAndArmors( int damage, int remainingArmor, int remainigHealth )
    {
        //Act
        _fighter.TakeDamage( damage );

        //Assert
        Assert.Equal( remainingArmor, _fighter.CurrentArmor );
        Assert.Equal( remainigHealth, _fighter.CurrentHealth );
    }
}
