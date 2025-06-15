using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class Cybertruck : IModel
{
    public IBrand Brand => new Tesla();

    public string Name => "Cybertruck";
}
