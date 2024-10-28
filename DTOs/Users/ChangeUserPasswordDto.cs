using System.ComponentModel.DataAnnotations;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Users
{
    public class ChangeUserPasswordDto
    {
        public string Id { get; set; }



        [Required(ErrorMessage = "*")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "*")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string ConfirmPassword { get; set; }



        public string Message { get; set; }

    }
}
