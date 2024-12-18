using CarsApp.Core.Domain;
using Microsoft.EntityFrameworkCore;


namespace CarsApp.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
