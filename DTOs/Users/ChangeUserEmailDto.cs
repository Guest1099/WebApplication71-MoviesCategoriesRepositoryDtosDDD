using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Users
{
    public class ChangeUserEmailDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }




        public string Message { get; set; }
    }
}
