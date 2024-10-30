using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowaniaDto : BaseSearchModel<GetLogowanieDto>
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataZalogowaniaOd { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month - 1,
            DateTime.Now.Day,
            12,
            0,
            0);

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataZalogowaniaDo { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            12,
            0,
            0);
    }
}
