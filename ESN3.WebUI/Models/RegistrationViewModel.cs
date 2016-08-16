using System.ComponentModel.DataAnnotations;

namespace ESN3.WebUI.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Enter user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords not compare")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter email")]
        public string Email { get; set; }
    }
}