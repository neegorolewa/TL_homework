using CarFactory.Models;
using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brands;
using CarFactory.Models.CarModels;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringPosition;
using CarFactory.Models.Transmissions;

namespace CarFactory;

public static class FactoryCar
{
    public static Car CreateCar()
    {
        Console.WriteLine( "Сборка машины начата..." );

        IBrand selectedBrand = SelectOption(
            "Автомобили в наличии:",
            new (string, IBrand)[]
            {
                ("BMW", new BMW()),
                ("Tesla", new Tesla()),
                ("Audi", new Audi()),
            } );

        IModel selectedModel = SelectModel( selectedBrand );

        IBody selectedBodyType = SelectOption(
            "Тип кузова в наличии:",
            new (string, IBody)[]
            {
                ("Купе", new Coupe()),
                ("Хэчбек", new Hatchback()),
                ("Седан", new Sedan()),
            } );

        IColor selectedColor = SelectOption(
            "Цвета в наличии:",
            new (string, IColor)[]
            {
                ("Красный", new Red()),
                ("Черный", new Black()),
                ("Синий", new Blue()),
            } );

        IEngine selectedEngine = SelectOption(
            "Тип двигателя в наличии:",
            new (string, IEngine)[]
            {
                ("Электрический", new Electric()),
                ("V-16", new V16()),
                ("V-8", new V8()),
            } );

        ITransmission selectedTransmission = SelectOption(
            "Тип кузова в наличии:",
            new (string, ITransmission)[]
            {
                ("Автомат", new Automat()),
                ("Механика", new Mechanical()),
            } );

        IPosition selectedSteeringPos = SelectOption(
            "Тип руля в наличии:",
            new (string, IPosition)[]
            {
                ("Левый", new LeftPosition()),
                ("Правый", new RightPosition()),
            } );

        Car newCar = new Car( selectedBrand, selectedModel, selectedBodyType, selectedColor, selectedEngine, selectedSteeringPos, selectedTransmission );

        return newCar;
    }

    public static T SelectOption<T>( string outMessage, (string typeMessage, T typeValue)[] options )
    {
        Console.WriteLine( outMessage );

        for ( int i = 0; i < options.Length; i++ )
        {
            Console.WriteLine( $"{i} - {options[ i ].typeMessage}" );
        }

        int indexSelectedOption = GetIntValue( 0, options.Length - 1 );

        return options[ indexSelectedOption ].typeValue;
    }

    public static IModel SelectModel( IBrand brand )
    {
        (string, IModel)[] models = brand.Name switch
        {
            "BMW" =>
            [
                ("M5", new M5()),
                ("X3", new X3()),
            ],
            "Tesla" =>
            [
                ("Cybertruck", new Cybertruck()),
                ("Model X", new ModelX()),
            ],
            "Audi" =>
            [
                ("Q8", new Q8()),
                ("A5", new A5()),
            ],
            _ =>
            [
                ("M5", new M5()),
                ("X3", new X3()),
            ],
        };

        return SelectOption( $"Модели {brand.Name} в наличии", models );
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
