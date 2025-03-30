namespace TransportWorldSystem.Web.Models
{
    public class DriversViewModel
    {
        public string Title { get; set; }
        public List<Driver> DriversList { get; set; }
    }

    public class Driver
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Vehicle { get; set; }
        public int ExperienceYears { get; set; }
    }
}