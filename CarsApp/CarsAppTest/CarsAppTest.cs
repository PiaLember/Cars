using CarsApp.Core.Dto;
using CarsApp.Core.ServiceInterface;

namespace CarsAppTest
{
    public class CarsAppTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_CreateEmpty_WhenCreated()
        {
            CarDto car = new();

            car.Make = "Mercedes-Benz";
            car.Model = "R107";
            car.Color = "Blue";
            car.Year = 1971;
            car.Fuel = "Diesel";
            car.Transmission = "A";
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;

            var result = await Svc<ICarsServices>().Create(car);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_DeleteByIdCar_whenDeleteCar()
        {

            CarDto car = MockCarData();

            var addCar = await Svc<ICarsServices>().Create(car);
            var result = await Svc<ICarsServices>().Delete((Guid)addCar.Id);


            Assert.Equal(result, addCar);
        }

        [Fact]
        public async Task ShouldNot_DeleteById_WhenNotDeleted()
        {
            CarDto car = MockCarData();

            var Car1 = await Svc<ICarsServices>().Create(car);
            var Car2 = await Svc<ICarsServices>().Create(car);

            var result = await Svc<ICarsServices>().Delete((Guid)Car1.Id);

            Assert.NotEqual(Car2.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateData_WhenUpdated()
        {
            CarDto dto = MockCarData();
            await Svc<ICarsServices>().Create(dto);

            CarDto update = UpdateCarData();
            await Svc<ICarsServices>().Update(update);

            Assert.DoesNotMatch(update.Make, dto.Make);
            Assert.NotEqual(update.ModifiedAt, dto.ModifiedAt);
        }

        private CarDto MockCarData()
        {
            CarDto car = new()
            {
                Make = "Qwerty",
                Model = "asd",
                Color = "Yellow",
                Year = 1970,
                Fuel = "Diesel",
                Transmission = "M",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            return car;
        }

        private CarDto UpdateCarData()
        {
            CarDto car = new()
            {
                Make = "Asd",
                Model = "Q",
                Color = "Black",
                Year = 1970,
                Fuel = "Petrol",
                Transmission = "A",
                CreatedAt = DateTime.Now.AddYears(5),
                ModifiedAt = DateTime.Now.AddYears(5)
            };
            return car;
        }

    }
}