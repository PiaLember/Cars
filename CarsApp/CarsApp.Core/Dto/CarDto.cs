using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Core.Dto
{
    public class CarDto
    {
        public Guid? Id { get; set; }
        public string? Make { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
        public DateTime Year { get; set; }
        public string? Fuel { get; set; } = string.Empty;
        public string? Transmission { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
