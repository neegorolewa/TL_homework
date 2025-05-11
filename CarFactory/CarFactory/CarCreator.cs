using CarFactory.Models;
using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brands;
using CarFactory.Models.CarModels;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringPosition;
using CarFactory.Models.Transmissions;

namespace CarFactory;

public static class CarCreator
{
    private static readonly Dictionary<string, IBrand> BrandOptions = new()
    {
        [ "BMW" ] = new BMW(),
        [ "Tesla" ] = new Tesla(),
        [ "Audi" ] = new Audi()
    };

    private static readonly Dictionary<string, IBody> BodyOptions = new()
    {
        [ "Купе" ] = new Coupe(),
        [ "Хэчбек" ] = new Hatchback(),
        [ "Седан" ] = new Sedan()
    };

    private static readonly Dictionary<string, IColor> ColorOptions = new()
    {
        [ "Красный" ] = new Red(),
        [ "Черный" ] = new Black(),
        [ "Синий" ] = new Blue()
    };

    private static readonly Dictionary<string, IEngine> EngineOptions = new()
    {
        [ "Электрический" ] = new Electric(),
        [ "V-16" ] = new V16(),
        [ "V-8" ] = new V8()
    };

    private static readonly Dictionary<string, ITransmission> TransmissionOptions = new()
    {
        [ "Автомат" ] = new Automat(),
        [ "Механика" ] = new Mechanical()
    };

    private static readonly Dictionary<string, IPosition> PositionOptions = new()
    {
        [ "Левый" ] = new LeftPosition(),
        [ "Правый" ] = new RightPosition()
    };

    private static readonly Dictionary<string, IModel> BmwModels = new()
    {
        [ "M5" ] = new M5(),
        [ "X3" ] = new X3()
    };

    private static readonly Dictionary<string, IModel> TeslaModels = new()
    {
        [ "Cybertruck" ] = new Cybertruck(),
        [ "Model X" ] = new ModelX()
    };

    private static readonly Dictionary<string, IModel> AudiModels = new()
    {
        [ "Q8" ] = new Q8(),
        [ "A5" ] = new A5()
    };

    private static readonly Dictionary<string, IModel> DefaultModels = new()
    {
        [ "M5" ] = new M5(),
        [ "X3" ] = new X3()
    };

    public static Car CreateCar()
    {
        Console.WriteLine( "Сборка машины начата..." );

        Console.WriteLine( "Автомобили в наличии:" );
        IBrand selectedBrand = SelectOption( BrandOptions );

        IModel selectedModel = SelectModel( selectedBrand );

        Console.WriteLine( "Тип кузова в наличии:" );
        IBody selectedBodyType = SelectOption( BodyOptions );

        Console.WriteLine( "Цвета в наличии:" );
        IColor selectedColor = SelectOption( ColorOptions );

        Console.WriteLine( "Тип двигателя в наличии:" );
        IEngine selectedEngine = SelectOption( EngineOptions );

        Console.WriteLine( "Тип кузова в наличии:" );
        ITransmission selectedTransmission = SelectOption( TransmissionOptions );

        Console.WriteLine( "Тип руля в наличии:" );
        IPosition selectedSteeringPos = SelectOption( PositionOptions );

        Car newCar = new Car( selectedBrand, selectedModel, selectedBodyType, selectedColor, selectedEngine, selectedSteeringPos, selectedTransmission );

        return newCar;
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

    public static IModel SelectModel( IBrand brand )
    {
        Dictionary<string, IModel> models = brand.Name switch
        {
            "BMW" => BmwModels,
            "Tesla" => TeslaModels,
            "Audi" => AudiModels,
            _ => DefaultModels,
        };

        Console.WriteLine( $"Модели {brand.Name} в наличии" );
        return SelectOption( models );
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
