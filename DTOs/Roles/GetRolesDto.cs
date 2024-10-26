using System.Collections.Generic;

namespace WebApplication71.DTOs.Roles
{
    public class GetRolesDto : BaseSearchModel<GetRoleDto>
    {
        public List<GetRoleDto> Users { get; set; }
    }
}
