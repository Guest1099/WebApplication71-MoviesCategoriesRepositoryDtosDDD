using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace WebApplication71.DTOs.Logowania
{
    public class CreateLogowanieDto
    {
        public DateTime DataLogowania { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            12,
            0,
            0);

        public DateTime DataWylogowania { get; set; } = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            12,
            0,
            0);

        public string UserId { get; set; }


        public SelectList UsersList { get; set; }
    }
}
