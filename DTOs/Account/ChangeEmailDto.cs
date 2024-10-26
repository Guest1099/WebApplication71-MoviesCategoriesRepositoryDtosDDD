using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Account
{
    public class ChangeEmailDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }

        public bool Success { get; set; }

        public string Result { get; set; }
    }
}
