﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Users
{
    public class GetUserDto
    {
        public string Id { get; set; }


        [DataType(DataType.EmailAddress)]
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
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi być w formacie XX-XXX, np. 12-345")]
        public string KodPocztowy { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Numer PESEL musi składać się z 11 cyfr")]
        public string Pesel { get; set; }


        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }


        public Plec Plec { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^(\d{3} ?\d{3} ?\d{3})$", ErrorMessage = "Numer telefonu musi składać się wyłącznie z cyfr, z opcjonalnymi spacjami.")]
        public string Telefon { get; set; }



        public string RoleName { get; set; }


        public DateTime DataDodania { get; set; }



        public List<PhotoUser> PhotosUser { get; set; }

        public List <IFormFile> Files { get; set; }
        public SelectList RolesList { get; set; }
    }
}
