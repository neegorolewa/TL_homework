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
    public static Car CreateCar()
    {
        Console.WriteLine( "Сборка машины начата..." );

        Console.WriteLine( "Автомобили в наличии:" );
        IBrand selectedBrand = SelectOption(
            new Dictionary<string, IBrand>
            {
                [ "BMW" ] = new BMW(),
                [ "Tesla" ] = new Tesla(),
                [ "Audi" ] = new Audi()
            } );

        IModel selectedModel = SelectModel( selectedBrand );

        Console.WriteLine( "Тип кузова в наличии:" );
        IBody selectedBodyType = SelectOption(
            new Dictionary<string, IBody>
            {
                [ "Купе" ] = new Coupe(),
                [ "Хэчбек" ] = new Hatchback(),
                [ "Седан" ] = new Sedan()
            } );

        Console.WriteLine( "Цвета в наличии:" );
        IColor selectedColor = SelectOption(
            new Dictionary<string, IColor>
            {
                [ "Красный" ] = new Red(),
                [ "Черный" ] = new Black(),
                [ "Синий" ] = new Blue()
            } );

        Console.WriteLine( "Тип двигателя в наличии:" );
        IEngine selectedEngine = SelectOption(
            new Dictionary<string, IEngine>
            {
                [ "Электрический" ] = new Electric(),
                [ "V-16" ] = new V16(),
                [ "V-8" ] = new V8()
            } );

        Console.WriteLine( "Тип кузова в наличии:" );
        ITransmission selectedTransmission = SelectOption(
            new Dictionary<string, ITransmission>
            {
                [ "Автомат" ] = new Automat(),
                [ "Механика" ] = new Mechanical()
            } );

        Console.WriteLine( "Тип руля в наличии:" );
        IPosition selectedSteeringPos = SelectOption(
            new Dictionary<string, IPosition>
            {
                [ "Левый" ] = new LeftPosition(),
                [ "Правый" ] = new RightPosition()
            } );

        Car newCar = new Car( selectedBrand, selectedModel, selectedBodyType, selectedColor, selectedEngine, selectedSteeringPos, selectedTransmission );

        return newCar;
    }

    public static T SelectOption<T>( Dictionary<string, T> options )
    {
        int index = 0;
        foreach ( (string key, T value) in options )
        {
            Console.WriteLine( $"{index++} - {key}" );
        }

        int indexSelectedOption = ReadIntInRange( 0, options.Count - 1 );

        return options.Values.ElementAt( indexSelectedOption );
    }

    public static IModel SelectModel( IBrand brand )
    {
        Dictionary<string, IModel> models = brand.Name switch
        {
            "BMW" => new Dictionary<string, IModel>
            {
                [ "M5" ] = new M5(),
                [ "X3" ] = new X3()
            },
            "Tesla" => new Dictionary<string, IModel>
            {
                [ "Cybertruck" ] = new Cybertruck(),
                [ "Model X" ] = new ModelX()
            },
            "Audi" => new Dictionary<string, IModel>
            {
                [ "Q8" ] = new Q8(),
                [ "A5" ] = new A5()
            },
            _ => new Dictionary<string, IModel>
            {
                [ "M5" ] = new M5(),
                [ "X3" ] = new X3()
            }
        };

        Console.WriteLine( $"Модели {brand.Name} в наличии" );
        return SelectOption( models );
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
