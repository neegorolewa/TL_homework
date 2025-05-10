namespace CarFactory.Models.Transmissions;

public class Mechanical : ITransmission
{
    public string Name => "Механическая";

    public int CountGear => 5;
}
