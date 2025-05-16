using System.Linq;
using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public class FightersFactory
{
    private static readonly Dictionary<string, IRace> RaceOptions = new()
    {
        [ "Русский" ] = new Russian(),
        [ "Грек" ] = new Greek(),
        [ "Монгол" ] = new Mongol(),
        [ "Татарин" ] = new Tatar()
    };

    private static readonly Dictionary<string, IClass> ClassOptions = new()
    {
        [ "Лучник" ] = new Archer(),
        [ "Рыцарь" ] = new Knight(),
        [ "Воин" ] = new Warrior()
    };

    private static readonly Dictionary<string, IArmor> ArmorOptions = new()
    {
        [ "Без брони" ] = new NoArmor(),
        [ "Нагрудник" ] = new Bib(),
        [ "Шлем" ] = new Helmet(),
        [ "Молитвенник" ] = new Prayer(),
    };

    private static readonly Dictionary<string, IWeapon> WeaponOptions = new()
    {
        [ "Без оружия" ] = new NoWeapon(),
        [ "Пистолет" ] = new Gun(),
        [ "Меч" ] = new Sword(),
        [ "Камень" ] = new Stone(),
        [ "Черная магия" ] = new Magic(),
    };

    public static IFighter CreateFighter()
    {
        Console.Write( "Введите имя бойца: " );
        string name = GetStringValue();

        Console.WriteLine( "Выберите расу из списка ниже:" );
        IRace selectedRace = SelectOption( RaceOptions );

        Console.WriteLine( "Выберите класс бойца из списка ниже:" );
        IClass selectedClass = SelectOption( ClassOptions );

        Console.WriteLine( "Выберите броню из списка ниже:" );
        IArmor selectedArmor = SelectOption( ArmorOptions );

        Console.WriteLine( "Выберите оружие из списка ниже:" );
        IWeapon selectedWeapon = SelectOption( WeaponOptions );

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

        int selectedIndex;
        do
        {
            selectedIndex = GetIntValue();
        }
        while ( !IsInRange( selectedIndex, 0, options.Count - 1 ) );

        return options.Values.ElementAt( selectedIndex );
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

    private static int GetIntValue()
    {
        while ( true )
        {
            if ( !int.TryParse( Console.ReadLine(), out int value ) )
            {
                Console.WriteLine( $"Введите числовое значение" );
                continue;
            }

            return value;
        }
    }

    private static bool IsInRange( int value, int minValue, int maxValue )
    {
        if ( value < minValue || value > maxValue )
        {
            Console.WriteLine( $"Введите значени от {minValue} до {maxValue} " );
            return false;
        }

        return true;
    }
}