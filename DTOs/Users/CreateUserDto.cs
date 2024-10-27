using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Users
{
    public class CreateUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Ulica { get; set; }

        [Required]
        public string Miejscowosc { get; set; }

        [Required]
        public string Wojewodztwo { get; set; }

        [Required]
        public string KodPocztowy { get; set; }

        [Required]
        public string Pesel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }

        [Required]
        public Plec Plec { get; set; }

        [Required]
        public string Telefon { get; set; } 
        public byte [] Photo { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string DataDodania { get; set; }




        public IFormFile PhotoData { get; set; }
        public SelectList RolesList { get; set; }
    }
}
