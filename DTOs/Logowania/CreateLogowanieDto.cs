using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using WebApplication71.DTOs.Users;

namespace WebApplication71.DTOs.Logowania
{
    public class CreateLogowanieDto
    {
        public DateTime DataLogowania { get; set; } = DateTime.Now;
        public DateTime DataWylogowania { get; set; } = DateTime.Now;
        public string UserId { get; set; }


        public SelectList UsersList { get; set; }
    }
}
