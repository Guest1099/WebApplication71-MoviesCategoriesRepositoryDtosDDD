using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Users
{
    public class GetUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string Miejscowosc { get; set; }
        public string Wojewodztwo { get; set; }
        public string KodPocztowy { get; set; }
        public string Pesel { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }
        public Plec Plec { get; set; }
        public string Telefon { get; set; }
        public byte[] Photo { get; set; }


        public string RoleName { get; set; }
        public DateTime DataDodania { get; set; }




        public IFormFile PhotoData { get; set; }
        public SelectList RolesList { get; set; }
    }
}
