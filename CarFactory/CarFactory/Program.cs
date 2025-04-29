using CarFactory;
using CarFactory.Models;

internal class Program
{
    private static void Main( string[] args )
    {
        CarCreator creator = new();

        Car car = creator.CreateCar();

        string carInfo = car.ToString();

        Console.WriteLine( carInfo );
    }
}