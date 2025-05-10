using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class A5 : IModel
{
    public IBrand Brand => new Audi();

    public string Name => "A5";
}
