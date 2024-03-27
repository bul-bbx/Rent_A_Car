using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Rent_A_Car.ViewModels
{
    public class EditRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Existing Role Name")]
        public string ExistingRoleName { get; set; }
    }
}
