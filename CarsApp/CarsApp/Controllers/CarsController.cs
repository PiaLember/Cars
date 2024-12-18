using CarsApp.Core.ServiceInterface;
using CarsApp.Data;
using CarsApp.Models.Cars;
using Microsoft.AspNetCore.Mvc;


namespace CarsApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context;
        private readonly ICarsServices _carsServices;

        public CarsController(CarContext context, ICarsServices cars)
        {
            _context = context;
            _carsServices = cars;
        }
        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    Color = x.Color,
                    Year = x.Year,
                    Fuel = x.Fuel,
                    Transmission = x.Transmission,

                }).ToList() ?? new List<CarIndexViewModel>();

            return View(result);
        }
    }
}