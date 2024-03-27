using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car.ViewModels
{
    public class DeleteRoleViewModel
    {
        [Required]
        [Display(Name = "Existing Role Name")]
        public string ExistingRoleName { get; set; }
    }
}
