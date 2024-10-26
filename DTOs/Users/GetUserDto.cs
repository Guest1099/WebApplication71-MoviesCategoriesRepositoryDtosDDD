using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Users
{
    public class GetUserDto
    {
        public string Email { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string Miejscowosc { get; set; }
        public string Wojewodztwo { get; set; }
        public string KodPocztowy { get; set; }
        public string Pesel { get; set; }
        public string DataUrodzenia { get; set; }
        public Plec Plec { get; set; }
        public string Telefon { get; set; }
        public string Photo { get; set; }
    }
}
