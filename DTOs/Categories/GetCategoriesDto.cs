using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Categories
{
    public class GetCategoriesDto : BaseSearchModel<GetCategoryDto>
    { 
        public SelectList SortowanieOptionItems = new SelectList(new List<string>() { "Nazwa A-Z", "Nazwa Z-A" });
    }
}
