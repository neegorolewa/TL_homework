using System.Linq;
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

        Console.WriteLine( "Выберите расу из списка ниже:" );
        IRace selectedRace = SelectOption(
            new Dictionary<string, IRace>
            {
                [ "Русский" ] = new Russian(),
                [ "Грек" ] = new Greek(),
                [ "Монгол" ] = new Mongol(),
                [ "Татарин" ] = new Tatar()
            } );

        Console.WriteLine( "Выберите класс бойца из списка ниже:" );
        IClass selectedClass = SelectOption(
            new Dictionary<string, IClass>
            {
                [ "Лучник" ] = new Archer(),
                [ "Рыцарь" ] = new Knight(),
                [ "Воин" ] = new Warrior()
            } );

        Console.WriteLine( "Выберите броню из списка ниже:" );
        IArmor selectedArmor = SelectOption(
            new Dictionary<string, IArmor>
            {
                [ "Без брони" ] = new NoArmor(),
                [ "Нагрудник" ] = new Bib(),
                [ "Шлем" ] = new Helmet(),
                [ "Молитвенник" ] = new Prayer(),
            } );

        Console.WriteLine( "Выберите оружие из списка ниже:" );
        IWeapon selectedWeapon = SelectOption(
            new Dictionary<string, IWeapon>
            {
                [ "Без оружия" ] = new NoWeapon(),
                [ "Пистолет" ] = new Gun(),
                [ "Меч" ] = new Sword(),
                [ "Камень" ] = new Stone(),
                [ "Черная магия" ] = new Magic(),
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

    private static T SelectOption<T>( Dictionary<string, T> options )
    {
        int index = 0;
        foreach ( (string key, T value) in options )
        {
            Console.WriteLine( $"{index++} - {key}" );
        }


        int indexSelectedOption = ReadIntInRange( 0, options.Count - 1 );
        return options.Values.ElementAt( indexSelectedOption );
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

    private static int ReadIntInRange( int minValue, int maxValue )
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