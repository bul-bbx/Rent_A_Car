using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rent_A_Car.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;

namespace Rent_A_Car.Data
{
    public class Car
    {
        [Key]
        [Required]
        [Display(Name = "Registration Number")]
        public int CarId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Brand { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Model { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Year { get; set; }
        [Required]
        [Column(TypeName = "tinyint")]
        [Display(Name = "Passenger Capacity")]
        public int PassengerCapacity { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Rental Price Per Day")]
        public decimal RentalPricePerDay { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }
        [NotMapped]
        [Display(Name = "Car Image")]
        public IFormFile? CarImage { get; set; }
        public string? CarImageUrl { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
