using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Users
{
    public class GetUsersDto : BaseSearchModel<GetUserDto>
    {
        public List <GetUserDto> Users { get; set; }
        public SelectList SelectListSearchOptionItems { get; set; } = new SelectList(new List<string>() { "Email", "Nazwisko", "Wszędzie" }, "Wszędzie");
        public SelectList SortowanieOptionItems { get; set; } = new SelectList(new List<string>() { "Email A-Z", "Email Z-A", "Nazwisko A-Z", "Nazwisko Z-A" }, "Email A-Z");
    }
}
