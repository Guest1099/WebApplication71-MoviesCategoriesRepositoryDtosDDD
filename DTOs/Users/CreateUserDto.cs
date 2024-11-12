using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string Password { get; set; }



        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Imie { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Nazwisko { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Ulica { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Miejscowosc { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Wojewodztwo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi być w formacie XX-XXX, np. 12-345")]
        public string KodPocztowy { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Numer PESEL musi składać się z 11 cyfr")]
        public string Pesel { get; set; }


        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; } = DateTime.Now;


        public Plec Plec { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^(\d{3} ?\d{3} ?\d{3})$", ErrorMessage = "Numer telefonu musi składać się wyłącznie z cyfr, z opcjonalnymi spacjami.")]
        public string Telefon { get; set; }


        public byte[] Photo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string RoleName { get; set; }

        public DateTime DataDodania { get; set; }





        public IFormFile PhotoData { get; set; }
        public SelectList RolesList { get; set; }
    }
}
