using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Account
{
    public class UpdateAccountDto
    {
        public string Id { get; set; }


        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Ulica { get; set; }

        [Required]
        public string Miejscowosc { get; set; }

        [Required]
        public string KodPocztowy { get; set; }

        [Required]
        public string Wojewodztwo { get; set; }

        [Required]
        public string Pesel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }

        [Required]
        public Plec Plec { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public byte[] Photo { get; set; }





        public IFormFile PhotoData { get; set; }
    }
}
