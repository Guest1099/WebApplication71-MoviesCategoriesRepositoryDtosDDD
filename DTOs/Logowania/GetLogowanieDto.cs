using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowanieDto
    {
        public string LogowanieId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataLogowania { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataWylogowania { get; set; }
        public TimeSpan CzasPracy { get; set; }



        public string Email { get; set; }
    }
}
