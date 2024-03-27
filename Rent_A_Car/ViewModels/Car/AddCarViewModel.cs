using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Rent_A_Car.ViewModels.Car
{
    public class AddCarViewModel
    {
        [Required]
        public string? Brand { get; set; }
        
        [Required]
        public string? Model { get; set; }
        
        [Required]
        public int Year { get; set; }
        
        [Required]
        [Display(Name = "Passenger Capacit")]
        public int PassengerCapacity { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Rental Price Per Day")]
        public decimal RentalPricePerDay { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Car Image")]
        public IFormFile? CarImage { get; set; }
        public string? CarImageUrl { get; set; }

    }
}
