using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public class FightersCreator
{
    public static IFighter CreateFighter()
    {
        Console.Write( "Введите имя бойца: " );
        string name = GetStringValue();

        IRace selectedRace = SelectRase();
        IClass selectedClass = SelectClass();
        IArmor selectedArmor = SelectArmor();
        IWeapon selectedWeapon = SelectWeapon();

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

    private static IWeapon SelectWeapon()
    {
        Console.WriteLine(
                $"""
                Выберите оружие из списка ниже:
                1 - Без оружия
                2 - Пистолет
                3 - Меч
                4 - Камень
                5 - Черная магия
                """
            );

        return GetIntValue( 1, 5 ) switch
        {
            1 => new NoWeapon(),
            2 => new Gun(),
            3 => new Sword(),
            4 => new Stone(),
            5 => new Magic(),
            _ => new NoWeapon()
        };
    }

    private static IArmor SelectArmor()
    {
        Console.WriteLine(
                $"""
                Выберите броню из списка ниже:
                1 - Без брони
                2 - Нагрудник
                3 - Шлем
                4 - Молитвенник
                """
            );

        return GetIntValue( 1, 4 ) switch
        {
            1 => new NoArmor(),
            2 => new Bib(),
            3 => new Helmet(),
            4 => new Prayer(),
            _ => new NoArmor()
        };
    }

    private static IClass SelectClass()
    {
        Console.WriteLine(
                $"""
                Выберите класс бойца из списка ниже:
                1 - Лучник
                2 - Рыцарь
                3 - Воин
                """
            );

        return GetIntValue( 1, 3 ) switch
        {
            1 => new Archer(),
            2 => new Knight(),
            3 => new Warrior(),
            _ => new Warrior()
        };
    }

    private static IRace SelectRase()
    {
        Console.WriteLine(
                $"""
                Выберите расу из списка ниже:
                1 - Русский
                2 - Грек
                3 - Монгол
                4 - Татарин
                """
            );

        return GetIntValue( 1, 4 ) switch
        {
            1 => new Russian(),
            2 => new Greek(),
            3 => new Mongol(),
            4 => new Tatar(),
            _ => new Russian()
        };
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
            if ( !int.TryParse( Console.ReadLine(), out int value ) && value < minValue && value > maxValue )
            {
                Console.WriteLine( $"Введите значение от {minValue} до {maxValue}" );
                continue;
            }

            return value;
        }
    }
}