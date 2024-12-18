using CarsApp.Core.Domain;


namespace CarsApp.Core.ServiceInterface
{
    public interface ICarsServices
    {
        Task<Car> DetailAsync(Guid id);
    }
}
