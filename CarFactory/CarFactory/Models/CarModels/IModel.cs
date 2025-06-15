using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public interface IModel
{
    public IBrand Brand { get; }
    public string Name { get; }
}
