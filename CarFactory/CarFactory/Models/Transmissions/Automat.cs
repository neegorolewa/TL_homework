namespace CarFactory.Models.Transmissions;

public class Automat : ITransmission
{
    public string Name => "Автомат";

    public int CountGear => 6;
}
