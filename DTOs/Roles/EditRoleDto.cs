using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Roles
{
    public class EditRoleDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }
    }
}
