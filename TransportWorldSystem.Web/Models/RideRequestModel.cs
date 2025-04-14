namespace TransportWorldSystem.Web.Models
{
    public class RideRequestModel
    {
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string RideType { get; set; } // Normal or Premium

        public double Distance { get; set; } // in kilometers
        public double Fare { get; set; }     // calculated based on distance and type

        public string DriverName { get; set; }
        public string DriverImageUrl { get; set; } // URL to the driver's picture
        public int EstimatedTime { get; set; }     // in minutes
        public int DriverIndex { get; internal set; }
    }
}
