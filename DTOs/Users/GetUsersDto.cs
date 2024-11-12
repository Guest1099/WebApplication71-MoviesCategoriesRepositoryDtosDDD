using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Users
{
    public class GetUsersDto : BaseSearchModel<GetUserDto>
    {
        public List<GetUserDto> Users { get; set; }
        public SelectList SelectListSearchOptionItems { get; set; }
        public SelectList SortowanieOptionItems { get; set; }
    }
}
