using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Supports
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DataType(DataType.Text)]
        public string ChangePasswordResult { get; set; }
    }
}
