using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication71.Services
{
    public class Paginator<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public Paginator(List<T> source, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(source);
        }

        public bool HasPreviousPage
            => PageIndex > 1;

        public bool HasNexPage
            => PageIndex < TotalPage;


        public static Paginator<T> CreateAsync(List<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            return
                new Paginator<T>(items, count, pageIndex, pageSize);
        }
    }
}
