
using CarsApp.Core.Dto;
using CarsApp.Core.ServiceInterface;
using CarsApp.Data;
using CarsApp.Models.Cars;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace CarsApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context; // Database context for accessing car data.
        private readonly ICarsServices _carsServices; //Service for business logic related to cars.
        private const int PageSize = 6;

        // Constructor to inject the database context and car services.
        public CarsController(CarContext context, ICarsServices cars)
        {
            _context = context;
            _carsServices = cars;
        }
        // Displays a list of cars in the Index view.
        public IActionResult Index(string sortOrder, string searchString, int pageNumber = 1)
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
                    Picture = x.Picture,

                });
            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(c => c.Model.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            result = sortOrder switch
            {
                "model_desc" => result.OrderByDescending(c => c.Model),
                "model_asc" => result.OrderBy(c => c.Model),
                "color_desc" => result.OrderByDescending(c => c.Color),
                "color_asc" => result.OrderBy(c => c.Color),
                "year_desc" => result.OrderByDescending(c => c.Year),
                "year_asc" => result.OrderBy(c => c.Year),
                "fuel_desc" => result.OrderByDescending(c => c.Fuel),
                "fuel_asc" => result.OrderBy(c => c.Fuel),
                "transmission_desc" => result.OrderByDescending(c => c.Transmission),
                "transmission_asc" => result.OrderBy(c => c.Transmission),
                _ => result.OrderBy(c => c.Year)
            };

            int totalItems = result.Count();
            result = result.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            // Pass pagination data to the view
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchString = searchString;

            return View(result.ToList());
        }

        // Displays the details of a specific car by its ID.
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
            vm.Picture = car.Picture;

            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;


            return View(vm);
        }

        // Displays the Create view to add a new car.
        [HttpGet]
        public IActionResult Create() => View("CreateUpdate", new CarCreateUpdateViewModel());

        // Handles the creation of a new car.
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
                    Picture = vm.Picture,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                };

                var result = await _carsServices.Create(dto); // Calls the service to create the car.

                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("CreateUpdate", vm);
        }

        // Displays the Update view for a specific car.
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarCreateUpdateViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Fuel = car.Fuel,
                Transmission = car.Transmission,
                Picture = car.Picture,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View("CreateUpdate", vm); // Returns the Update view with car data pre-filled.
        }

        // Handles the update of an existing car.
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new CarDto
                {
                    Id = vm.Id,
                    Make = vm.Make,
                    Model = vm.Model,
                    Color = vm.Color,
                    Year = vm.Year,
                    Fuel = vm.Fuel,
                    Transmission = vm.Transmission,
                    Picture = vm.Picture,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                };

                var result = await _carsServices.Update(dto); // Calls the service to update the car.

                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("CreateUpdate", vm);
        }

        // Displays the Delete view for a specific car.
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id); // Fetch car details.

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDeleteViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Fuel = car.Fuel,
                Transmission = car.Transmission,
                Picture = car.Picture,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        // Handles the deletion of a car after confirmation.
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id); // Calls the service to delete the car.

            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}