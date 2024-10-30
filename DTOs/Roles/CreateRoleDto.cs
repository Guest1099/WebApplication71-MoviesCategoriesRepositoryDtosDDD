using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Roles
{
    public class CreateRoleDto
    {

        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }
    }
}
