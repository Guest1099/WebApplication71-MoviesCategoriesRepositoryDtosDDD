using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowaniaDto : BaseSearchModel<GetLogowanieDto>
    {
        [Required]
        [DataType(DataType.DateTime)]
        public string DataZalogowaniaOd { get; set; } = DateTime.Now.ToString();

        [Required]
        [DataType(DataType.DateTime)]
        public string DataZalogowaniaDo { get; set; } = DateTime.Now.ToString();
    }
}
