namespace CarsApp.Models.Cars
{
    public class CarCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Fuel { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Picture {  get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
