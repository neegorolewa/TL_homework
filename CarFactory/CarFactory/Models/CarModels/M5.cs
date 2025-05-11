using CarFactory.Models.Brands;

namespace CarFactory.Models.CarModels;

public class M5 : IModel
{
    public IBrand Brand => new BMW();

    public string Name => "M5";
}
