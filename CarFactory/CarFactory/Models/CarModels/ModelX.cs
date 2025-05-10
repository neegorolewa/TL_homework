using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class ModelX : IModel
{
    public IBrand Brand => new Tesla();

    public string Name => "Model X";
}
