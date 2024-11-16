using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApplication71.Services;

namespace WebApplication71.DTOs
{
    public class BaseSearchModel<T>
    {
        // Opcje wyszukiwania, np. wyszukiwanie w emailach, albo w nazwiskach
        public string SearchOption { get; set; }

        // Wyszukiwarka
        public string q { get; set; }

        // Opcje sortowania np. Nazwa A-Z, Nazwa Z-A, i tak dalej
        public string SortowanieOption { get; set; }



        // Paginator
        public Paginator<T> Paginator { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Start { get; set; } = 1;
        public int End { get; set; } = 0;

        //public int IlePokazac { get; set; } = 0;
        public bool LastPage { get; set; } = false;
        public bool DisplayButtonLeftTrzyKropki { get; set; } = false;
        public bool DisplayButtonRightTrzyKropki { get; set; } = false;
        public bool ShowPaginator { get; set; } = true;
        public bool DisplayNumersListAndPaginatorLinks { get; set; } = true;
        public int IloscObecnychElementow5 { get; set; } = 0;
        public int IloscObecnychElementow10 { get; set; } = 0;
        public int IloscObecnychElementow15 { get; set; } = 0;
        public int IloscObecnychElementow20 { get; set; } = 0;
        public int Klikniecie { get; set; } = 0;




        public SelectList SelectListNumberItems { get; set; } = new SelectList(new List<string>() { "5", "10", "15", "20" });

    }
}
