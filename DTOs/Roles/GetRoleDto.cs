using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication71.DTOs.Users;

namespace WebApplication71.DTOs.Roles
{
    public class GetRoleDto
    {
        public string Id { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }




        public List<GetUserDto> Users { get; set; }
    }
}
