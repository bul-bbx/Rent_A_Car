using Rent_A_Car.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_A_Car.Data
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        [ForeignKey("Id")]
        public string UserId { get; set; }
        [Column(TypeName = "int")]
        [ForeignKey("UserId")]
        public int CarId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Car Car { get; set; }

        // Reservation details
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
