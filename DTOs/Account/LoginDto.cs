using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        //public string LoginResult { get; set; }
    }
}
