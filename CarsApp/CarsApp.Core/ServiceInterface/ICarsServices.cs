using CarsApp.Core.Domain;
using CarsApp.Core.Dto;


namespace CarsApp.Core.ServiceInterface
{
    public interface ICarsServices
    {
        Task<Car> Create(CarDto dto);
        Task<Car> DetailsAsync(Guid id);
        Task<Car> Update(CarDto dto);
        Task<Car> Delete(Guid id);
    }
}
