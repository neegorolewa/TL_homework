using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.Tests.FightersFactory;
public class FightersFactoryTests
{
    [Fact]
    public void CreateFighter_ValidInput_CreateFighterWithCorrectProperties()
    {
        //Arrange
        string input =
            """
            NameFighter
            0
            0
            0
            0
            """;

        var stringReader = new StringReader( input );
        var stringWriter = new StringWriter();

        var originalIn = Console.In;
        var originalOut = Console.Out;

        try
        {
            Console.SetIn( stringReader );
            Console.SetOut( stringWriter );

            //Act
            IFighter fighter = Fighters.FightersFactory.CreateFighter();

            //Assert
            string output = stringWriter.ToString();
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
        finally
        {
            Console.SetIn( originalIn );
            Console.SetOut( originalOut );
        }
    }

    [Fact]
    public void CreateFighter_EmptyInputPropertiesThenValidInputThenValidParameters_ReturnsMessageFromMethodGetStringValue()
    {
        // Arrange
        string input =
            """

            NameFighter
            0
            0
            0
            0
            """;

        var stringReader = new StringReader( input );
        var stringWriter = new StringWriter();

        var originalIn = Console.In;
        var originalOut = Console.Out;

        try
        {
            Console.SetIn( stringReader );
            Console.SetOut( stringWriter );

            // Act
            IFighter fihgter = Fighters.FightersFactory.CreateFighter();

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains( "Поле не может быть пустым. Пожалуйста введите значение: ", output );
        }
        finally
        {
            Console.SetIn( originalIn );
            Console.SetOut( originalOut );
        }
    }

    [Fact]
    public void CreateFighter_StringValueWhenShouldBeIntValueThenValidParameters_ReturnsMessageFromMethodGetIntValue()
    {
        // Arrange
        string input =
            """
            NameFighter
            race
            0
            0
            0
            0
            """;

        var stringReader = new StringReader( input );
        var stringWriter = new StringWriter();

        var originalIn = Console.In;
        var originalOut = Console.Out;

        try
        {
            Console.SetIn( stringReader );
            Console.SetOut( stringWriter );

            // Act
            IFighter fihgter = Fighters.FightersFactory.CreateFighter();

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains( "Введите числовое значение", output );
        }
        finally
        {
            Console.SetIn( originalIn );
            Console.SetOut( originalOut );
        }
    }

    [Fact]
    public void CreateFighter_ValueNotInRangeThenValidParameters_ReturnsMessageFromMethodIsInRange()
    {
        // Arrange
        string input =
            """
            NameFighter
            0
            0
            9
            0
            0
            0
            """;

        var stringReader = new StringReader( input );
        var stringWriter = new StringWriter();

        var originalIn = Console.In;
        var originalOut = Console.Out;

        try
        {
            Console.SetIn( stringReader );
            Console.SetOut( stringWriter );

            // Act
            IFighter fihgter = Fighters.FightersFactory.CreateFighter();

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains( "Введите значени от 0 до 3", output );
        }
        finally
        {
            Console.SetIn( originalIn );
            Console.SetOut( originalOut );
        }
    }
}
