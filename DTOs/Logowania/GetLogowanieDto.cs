using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowanieDto
    {
        public string LogowanieId { get; set; }



        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataLogowania { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataWylogowania { get; set; }

        public TimeSpan CzasPracy { get; set; }
        public string ImieInazwisko { get; set; }

        public string Email { get; set; }
    }
}
