using CarFactory.Models;
using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brands;
using CarFactory.Models.CarModels;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringPosition;
using CarFactory.Models.Transmissions;

namespace CarFactory;

public class CarCreator
{
    public Car CreateCar()
    {
        Console.WriteLine( "Сборка машины начата..." );

        IBrand selectedBrand = SelectBrand();
        IModel selectedModel = SelectModel( selectedBrand );
        IBody selectedBodyType = SelectBodyShapes();
        IColor selectedColor = SelectColor();
        IEngine selectedEngine = SelectEngine();
        ITransmission selectedTransmission = SelectTransmission();
        IPosition selectedSteeringPos = SelectSteeringPosition();

        Car newCar = new Car( selectedBrand, selectedModel, selectedBodyType, selectedColor, selectedEngine, selectedSteeringPos, selectedTransmission );

        return newCar;
    }

    private IPosition SelectSteeringPosition()
    {
        Console.WriteLine(
             $"""
            Тип руля в наличии:
            (1) - Левый
            (2) - Правый
            """
         );

        return GetIntValue( 1, 2 ) switch
        {
            1 => new LeftPosition(),
            2 => new RightPosition(),
            _ => new LeftPosition(),
        };
    }

    private ITransmission SelectTransmission()
    {
        Console.WriteLine(
            $"""
            Тип кузова в наличии:
            (1) - Автомат
            (2) - Механика
            """
        );

        return GetIntValue( 1, 2 ) switch
        {
            1 => new Automat(),
            2 => new Mechanical(),
            _ => new Automat(),
        };
    }

    private IBody SelectBodyShapes()
    {
        Console.WriteLine(
            $"""
            Тип кузова в наличии:
            (1) - Купе
            (2) - Хэчбек
            (3) - Седан
            """
        );

        return GetIntValue( 1, 3 ) switch
        {
            1 => new Coupe(),
            2 => new Hatchback(),
            3 => new Sedan(),
            _ => new Coupe(),
        };
    }

    public static IBrand SelectBrand()
    {
        Console.WriteLine(
            $"""
            Автомобили в наличии:
            (1) - BMW
            (2) - Tesla
            (3) - Audi
            """
        );

        return GetIntValue( 1, 3 ) switch
        {
            1 => new BMW(),
            2 => new Tesla(),
            3 => new Audi(),
            _ => new BMW(),
        };
    }

    public static IColor SelectColor()
    {
        Console.WriteLine(
            $"""
            Цвета:
            (1) - Красный
            (2) - Черный
            (3) - Синий
            """
        );

        return GetIntValue( 1, 3 ) switch
        {
            1 => new Red(),
            2 => new Black(),
            3 => new Blue(),
            _ => new Black(),
        };
    }

    public static IEngine SelectEngine()
    {
        Console.WriteLine(
            $"""
            Тип двигателя в наличии:
            (1) - Электрический
            (2) - V-16
            (3) - V-8
            """
        );

        return GetIntValue( 1, 3 ) switch
        {
            1 => new Electric(),
            2 => new V16(),
            3 => new V8(),
            _ => new Electric(),
        };
    }

    public static IModel SelectModel( IBrand brand )
    {
        Console.WriteLine( PrintModel( brand ) );

        switch ( brand.Name )
        {
            case "BMW":
                return GetIntValue( 1, 2 ) switch
                {
                    1 => new M5(),
                    2 => new X3(),
                    _ => new X3(),
                };
            case "Tesla":
                return GetIntValue( 1, 2 ) switch
                {
                    1 => new ModelX(),
                    2 => new Cybertruck(),
                    _ => new ModelX(),
                };
            case "Audi":
                return GetIntValue( 1, 2 ) switch
                {
                    1 => new Q8(),
                    2 => new A5(),
                    _ => new Q8(),
                };
            default: return new M5();
        }
    }

    public static string PrintModel( IBrand brand )
    {
        List<string> listModels = new( GetModelOfBrand( brand ) );
        string message = "Модели в наличии\n";
        for ( int i = 0; i < listModels.Count; i++ )
        {
            if ( i < 1 )
            {
                message += $"({i + 1}) - {listModels[ i ]}\n";
            }
            else
            {
                message += $"({i + 1}) - {listModels[ i ]}";
            }
        }
        return message;
    }

    public static List<string> GetModelOfBrand( IBrand brand )
    {
        return ( brand.Name ) switch
        {
            "BMW" => new List<string> { "M5", "X3" },
            "Tesla" => new List<string> { "Model X", "Cybertruck" },
            "Audi" => new List<string> { "Q8", "A5" },
            _ => new List<string> { "M5", "X3" },
        };
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
