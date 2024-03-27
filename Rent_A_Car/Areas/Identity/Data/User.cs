using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rent_A_Car.Data;

namespace Rent_A_Car.Areas.Identity.Data;

public class User : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Lest Name")]
    public string LastName { get; set; }

    [ProtectedPersonalData]
    [Column(TypeName = "nchar(10)")]
    public string EGN {  get; set; }


    public ICollection<Reservation> Reservations { get; set; }
}

