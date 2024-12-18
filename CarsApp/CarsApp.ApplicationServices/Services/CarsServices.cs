using CarsApp.Core.Domain;
using CarsApp.Core.ServiceInterface;
using CarsApp.Data;
using Microsoft.EntityFrameworkCore;


namespace CarsApp.ApplicationServices.Services
{
    public class CarsServices

    {
        private readonly CarContext _context;

        public CarsServices(CarContext context)
        {
            _context = context;
        }

    }
}
