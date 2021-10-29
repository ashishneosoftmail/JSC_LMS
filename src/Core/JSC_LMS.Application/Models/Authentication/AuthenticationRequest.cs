using System.ComponentModel.DataAnnotations;

namespace JSC_LMS.Application.Models.Authentication
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        public string Role { get; set; }
    }
}
