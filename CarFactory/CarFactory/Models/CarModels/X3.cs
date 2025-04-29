using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class X3 : IModel
{
    public IBrand Brand => new BMW();

    public string Name => "X3";
}
