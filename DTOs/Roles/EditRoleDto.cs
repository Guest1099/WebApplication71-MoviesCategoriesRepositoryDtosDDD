using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Roles
{
    public class EditRoleDto
    {

        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }
    }
}
