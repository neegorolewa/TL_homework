using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brands;
using CarFactory.Models.CarModels;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringPosition;
using CarFactory.Models.Transmissions;

namespace CarFactory.Models;

public class Car
{
    public IBrand Brand { get; private set; }
    public IModel Model { get; private set; }
    public IBody Body { get; private set; }
    public IColor Color { get; private set; }
    public IEngine Engine { get; private set; }
    public IPosition SteeringPosition { get; private set; }
    public ITransmission Transmission { get; private set; }
    public int Speed { get; private set; }
    public int Gear { get; private set; }

    public Car( IBrand brand, IModel model, IBody body, IColor color, IEngine engine, IPosition steeringPosition, ITransmission transmission )
    {
        Brand = brand;
        Model = model;
        Body = body;
        Color = color;
        Engine = engine;
        SteeringPosition = steeringPosition;
        Transmission = transmission;
        Speed = Engine.MaxSpeed;
        Gear = Transmission.CountGear;
    }

    public override string ToString()
    {
        return
            $"""
            Название: {Brand.Name} {Model.Name}
            Тип кузова: {Body.Name}
            Цвет: {Color.Name}
            Двигатель: {Engine.Name}
            Тип руля: {SteeringPosition.Name}
            Коробка передач: {Transmission.Name}
            Максимальная скорость: {Speed}
            Количество передач: {Gear}
            """;
    }
}
