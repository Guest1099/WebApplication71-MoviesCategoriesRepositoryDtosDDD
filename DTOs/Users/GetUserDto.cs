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


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Email { get; set; }


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
        public string KodPocztowy { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Pesel { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateTime DataUrodzenia { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public Plec Plec { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public byte[] Photo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string RoleName { get; set; }

         
        public DateTime DataDodania { get; set; }




        public IFormFile PhotoData { get; set; }
        public SelectList RolesList { get; set; }
    }
}
