using Microsoft.AspNetCore.Identity;
using Rent_A_Car.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car.ViewModels
{
    public class AddUserToRoleViewModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
