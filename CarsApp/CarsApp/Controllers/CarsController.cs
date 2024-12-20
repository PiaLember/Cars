
using CarsApp.Core.Dto;
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Model = car.Model;
            vm.Color = car.Color;
            vm.Year = car.Year;    
            vm.Fuel = car.Fuel;
            vm.Transmission = car.Transmission;

            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;


            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CarCreateUpdateViewModel();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new CarDto
                {
                    Make = vm.Make,
                    Model = vm.Model,
                    Color = vm.Color,
                    Year = vm.Year,  
                    Fuel = vm.Fuel,
                    Transmission = vm.Transmission,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                };

                var result = await _carsServices.Create(dto);

                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("CreateUpdate", vm);
        }
    }
}