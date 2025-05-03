using System.Reflection.Metadata;

namespace TransportWorldSystem.Web.Data
{
    public class Drivers
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Rating { get; set; }
        public string VehicleType { get; set; } // e.g., Truck, Van, etc.
    }
}
