namespace CarFactory.Models.Transmissions;

public interface ITransmission
{
    public string Name { get; }
    public int CountGear {  get; }
}
