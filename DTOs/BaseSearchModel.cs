using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApplication71.Services;

namespace WebApplication71.DTOs
{
    public class BaseSearchModel<T>
    {

        // Wyszukiwarka  
        public string q { get; set; } = "";
        public string SearchOption { get; set; }
        public string SortowanieOption { get; set; }



        // Paginator
        public Paginator<T> Paginator { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int IlePokazac { get; set; } = 0;
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;


        public SelectList SelectListNumberItems { get; set; } = new SelectList(new List<string>() { "5", "10", "15", "20" });

    }
}
