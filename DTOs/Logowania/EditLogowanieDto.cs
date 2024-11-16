using System;
using System.ComponentModel.DataAnnotations;

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



        public string Email { get; set; }
    }
}
