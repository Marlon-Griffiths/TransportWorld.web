using System.ComponentModel.DataAnnotations;

namespace TransportWorldSystem.Web.Models
{
    public class RideRequestModel
    {
        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropoffLocation { get; set; }

        [Required]
        public string RideType { get; set; } // "Normal" or "Premium"

        public double Distance { get; set; }
        public double Fare { get; set; }
    }
}
