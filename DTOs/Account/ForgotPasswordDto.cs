using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public string Url { get; set; }
    }
}
