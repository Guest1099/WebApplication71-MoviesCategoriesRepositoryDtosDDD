using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Logowania
{
    public class CreateLogowanieDto
    {

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataLogowania { get; set; } = DateTime.Now;


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataWylogowania { get; set; } = DateTime.Now.AddHours(2);

        public StatusZalogowania Status { get; set; }
        public string UserId { get; set; }


        public SelectList UsersList { get; set; }
    }
}
