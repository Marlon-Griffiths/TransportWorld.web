namespace TransportWorldSystem.Web.Models
{
    public class RideRequestModel
    {
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string RideType { get; set; }
        public double Distance { get; set; }
        public double Fare { get; set; }
        public double EstimatedTime { get; set; }
        public string DriverName { get; set; }
        public string DriverImageUrl { get; set; }
    }
}
