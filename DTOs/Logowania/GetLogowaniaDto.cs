using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowaniaDto : BaseSearchModel<GetLogowanieDto>
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataZalogowaniaOd { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            6,
            0,
            0
            ).AddDays(-30);

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataZalogowaniaDo { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            12,
            0,
            0
            );


        public List<GetLogowanieDto> Logowania { get; set; }
        public SelectList SortowanieOptionItems = new SelectList(new List<string>() { "Data zalogowania rosnąco", "Data zalogowania malejąco" });
    }
}
