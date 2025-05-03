using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public class FightersFactory
{
    public static IFighter CreateFighter()
    {
        Console.Write( "Введите имя бойца: " );
        string name = GetStringValue();

        IRace selectedRace = SelectOption(
            "Выберите расу из списка ниже:",
            new (string, IRace)[]
            {
                ("Русский", new Russian()),
                ("Грек", new Greek()),
                ("Монгол", new Mongol()),
                ("Татарин", new Tatar()),
            } );

        IClass selectedClass = SelectOption(
            "Выберите класс бойца из списка ниже:",
            new (string, IClass)[]
            {
                ("Лучник", new Archer()),
                ("Рыцарь", new Knight()),
                ("Воин", new Warrior()),
            } );

        IArmor selectedArmor = SelectOption(
            "Выберите броню из списка ниже:",
            new (string, IArmor)[]
            {
                ("Без брони", new NoArmor()),
                ("Нагрудник", new Bib()),
                ("Шлем", new Helmet()),
                ("Молитвенник", new Prayer()),
            } );

        IWeapon selectedWeapon = SelectOption(
            "Выберите оружие из списка ниже:",
            new (string, IWeapon)[]
            {
                ("Без оружия", new NoWeapon()),
                ("Пистолет", new Gun()),
                ("Меч", new Sword()),
                ("Камень", new Stone()),
                ("Черная магия", new Magic()),
            } );

        IFighter newFighter = new Fighter( name, selectedRace, selectedClass, selectedArmor, selectedWeapon );

        Console.WriteLine(
                $"""
                Боец {name} создан с характеристикам:
                - Здоровье: {newFighter.MaxHealth}
                - Урон: {newFighter.Damage}
                - Броня: {newFighter.MaxArmor}
                """
            );

        return newFighter;
    }

    private static T SelectOption<T>( string outMessage, (string typeMessage, T typeValue)[] options )
    {
        Console.WriteLine( outMessage );

        for ( int i = 0; i < options.Length; i++ )
        {
            Console.WriteLine( $"{i} - {options[ i ].typeMessage}" );
        }

        int indexSelectedOption = GetIntValue( 0, options.Length - 1 );
        return options[ indexSelectedOption ].typeValue;
    }

    private static string GetStringValue()
    {
        while ( true )
        {
            string? value = Console.ReadLine();

            if ( string.IsNullOrEmpty( value ) )
            {
                Console.Write( "Поле не может быть пустым. Пожалуйста введите значение: " );
                continue;
            }

            return value;
        }
    }

    private static int GetIntValue( int minValue, int maxValue )
    {
        while ( true )
        {
            if ( !int.TryParse( Console.ReadLine(), out int value ) || ( value < minValue || value > maxValue ) )
            {
                Console.WriteLine( $"Введите значение от {minValue} до {maxValue}" );
                continue;
            }

            return value;
        }
    }
}