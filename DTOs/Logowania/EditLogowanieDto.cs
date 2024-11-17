using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Logowania
{
    public class EditLogowanieDto
    {
        public string LogowanieId { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataLogowania { get; set; } = DateTime.Now;

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataWylogowania { get; set; } = DateTime.Now;

        public TimeSpan CzasPracy { get; set; }

        public StatusZalogowania Status { get; set; }



        public string Email { get; set; }
    }
}
