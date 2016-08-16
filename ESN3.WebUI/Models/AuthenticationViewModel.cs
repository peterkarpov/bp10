using System.ComponentModel.DataAnnotations;

namespace ESN3.WebUI.Models
{
    public class AuthenticationViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}