using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Roles
{
    public class GetRolesDto : BaseSearchModel<GetRoleDto>
    {
        public SelectList SortowanieOptionItems = new SelectList(new List<string>() { "Nazwa A-Z", "Nazwa Z-A" });
    }
}
