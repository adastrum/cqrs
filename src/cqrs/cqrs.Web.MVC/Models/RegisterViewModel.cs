using System.ComponentModel.DataAnnotations;

namespace cqrs.Web.MVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-enter password")]
        public string ConfirmPassword { get; set; }
    }
}
