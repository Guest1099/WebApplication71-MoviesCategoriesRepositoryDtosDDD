using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string LoginResult { get; set; }
    }
}
