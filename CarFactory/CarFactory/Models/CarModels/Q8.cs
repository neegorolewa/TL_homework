using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class Q8 : IModel
{
    public IBrand Brand => new Audi();

    public string Name => "Q8";
}
