using System.ComponentModel.DataAnnotations;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Account
{
    public class ChangePasswordDto
    {

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string NewPassword { get; set; }


        public string Email { get; set; }
    }
}
