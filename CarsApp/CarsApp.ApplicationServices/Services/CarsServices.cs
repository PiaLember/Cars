using CarsApp.Core.Domain;
using CarsApp.Core.Dto;
using CarsApp.Core.ServiceInterface;
using CarsApp.Data;
using Microsoft.EntityFrameworkCore;


namespace CarsApp.ApplicationServices.Services
{
    public class CarsServices : ICarsServices

    {
        private readonly CarContext _context;

        public CarsServices(CarContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(CarDto dto)
        {

            Car car = new Car
            {

                Id = Guid.NewGuid(),
                Make = dto.Make,
                Model = dto.Model,
                Color = dto.Color,
                Year = dto.Year,
                Fuel = dto.Fuel,
                Transmission = dto.Transmission,               
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };


            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }
    }
}
