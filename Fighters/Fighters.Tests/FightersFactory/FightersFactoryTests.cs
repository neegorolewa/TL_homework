using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Tests.ConsoleMethods;

namespace Fighters.Tests.FightersFactory;
public class FightersFactoryTests : IClassFixture<ConsoleFixture>
{
    private static readonly List<string> _testsInput =
    [
         """
         NameFighter
         0
         0
         0
         0
         """,

         """
         
         NameFighter
         0
         0
         0
         0
         """,

         """
         NameFighter
         race
         0
         0
         0
         0
         """,

         """
         NameFighter
         0
         0
         9
         0
         0
         0
         """
    ];

    private readonly ConsoleFixture _consoleFixture;

    public FightersFactoryTests( ConsoleFixture consoleFixture )
    {
        _consoleFixture = consoleFixture;
        _consoleFixture.StringWriter.GetStringBuilder().Clear();
    }

    private void PrepareTest( int inputIndex )
    {
        _consoleFixture.SetInput( _testsInput[ inputIndex ] );
    }

    [Fact]
    public void CreateFighter_CreateCorrectsFighter()
    {
        //Arrange
        PrepareTest( 0 );

        //Act
        IFighter fighter = Fighters.FightersFactory.CreateFighter();

        //Assert
        string output = _consoleFixture.StringWriter.ToString();
        Assert.Contains( "Введите имя бойца: ", output );
        Assert.Contains( "Выберите расу из списка ниже:", output );
        Assert.Contains( "Выберите класс бойца из списка ниже:", output );
        Assert.Contains( "Выберите броню из списка ниже:", output );
        Assert.Contains( "Выберите оружие из списка ниже:", output );
        Assert.Contains( $"""
                Боец {fighter.Name} создан с характеристикам:
                - Здоровье: {fighter.MaxHealth}
                - Урон: {fighter.Damage}
                - Броня: {fighter.MaxArmor}
                """, output );

        Assert.NotNull( fighter );
        Assert.Equal( "NameFighter", fighter.Name );
        Assert.IsType<Russian>( fighter.Race );
        Assert.IsType<Archer>( fighter.Class );
        Assert.IsType<NoArmor>( fighter.Armor );
        Assert.IsType<NoWeapon>( fighter.Weapon );
    }

    [Fact]
    public void CreateFighter_EmptyStringBeforeValidInput_ReturnsMessageFromMethodGetStringValue()
    {
        // Arrange
        PrepareTest( 1 );

        // Act
        Fighters.FightersFactory.CreateFighter();

        // Assert
        string output = _consoleFixture.StringWriter.ToString();
        Assert.Contains( "Поле не может быть пустым. Пожалуйста введите значение: ", output );

    }

    [Fact]
    public void CreateFighter_StringValueWhenShouldBeIntValueThenValidParameters_ReturnsMessageFromMethodGetIntValue()
    {
        // Arrange
        PrepareTest( 2 );

        // Act
        Fighters.FightersFactory.CreateFighter();

        // Assert
        string output = _consoleFixture.StringWriter.ToString();
        Assert.Contains( "Введите числовое значение", output );

    }

    [Fact]
    public void CreateFighter_ValueNotInRangeThenValidParameters_ReturnsMessageFromMethodIsInRange()
    {
        // Arrange
        PrepareTest( 3 );

        // Act
        Fighters.FightersFactory.CreateFighter();

        // Assert
        string output = _consoleFixture.StringWriter.ToString();
        Assert.Contains( "Введите значени от 0 до 3", output );

    }
}
