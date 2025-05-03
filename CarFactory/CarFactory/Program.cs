using CarFactory;
using CarFactory.Models;

internal class Program
{
    private static void Main( string[] args )
    {

        Car car = FactoryCar.CreateCar();

        string carInfo = car.ToString();

        Console.WriteLine( carInfo );
    }
}