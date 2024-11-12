using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Categories
{
    public class GetCategoriesDto : BaseSearchModel<GetCategoryDto>
    {
        public List<GetCategoryDto> Categories { get; set; }
        public SelectList SortowanieOptionItems { get; set; }
    }
}
