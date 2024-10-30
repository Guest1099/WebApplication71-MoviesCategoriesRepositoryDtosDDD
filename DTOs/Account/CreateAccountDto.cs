﻿using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Account
{
    public class CreateAccountDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
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
        public DateTime DataUrodzenia { get; set; }

        [Required]
        public Plec Plec { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
