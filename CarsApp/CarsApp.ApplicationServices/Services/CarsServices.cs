using CarsApp.Core.Domain;
using CarsApp.Core.Dto;
using CarsApp.Core.ServiceInterface;
using CarsApp.Data;
using Microsoft.EntityFrameworkCore;


namespace CarsApp.ApplicationServices.Services
{
    public class CarsServices : ICarsServices

    {
        private readonly CarContext _context; // Database context for managing cars.

        // Constructor to inject the database context.
        public CarsServices(CarContext context)
        {
            _context = context;
        }

        // Creates a new car record in the database.
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
                Picture = dto.Picture,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };


            _context.Cars.Add(car); // Add the new car to the database context.
            await _context.SaveChangesAsync(); // Save changes to the database.

            return car;
        }

        // Retrieves the details of a car by its ID.
        public async Task<Car> DetailsAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        // Updates an existing car record in the database.
        public async Task<Car> Update(CarDto dto)
        {

            Car car = await _context.Cars.FindAsync(dto.Id); // Find the car by its ID.

            if (car == null)
            {
                return null;
            }
            // Update the car properties with values from the DTO.
            car.Make = dto.Make;
            car.Model = dto.Model;
            car.Color = dto.Color;
            car.Year = dto.Year;
            car.Fuel = dto.Fuel;
            car.Transmission = dto.Transmission; 
            car.Picture = dto.Picture;
            car.CreatedAt = dto.CreatedAt; // Preserve the original creation date.
            car.ModifiedAt = DateTime.Now; // Update the modified date to the current date.

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        // Deletes a car record from the database.
        public async Task<Car> Delete(Guid id)
        {

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return null;
            }


            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }
    }
}
