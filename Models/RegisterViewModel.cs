using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.Supports
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress), MinLength(5)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password), MinLength(10)]
        public string Password { get; set; }


        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Photo { get; set; }
        public string Telefon { get; set; }
        public string DataUrodzenia { get; set; }
        public string Ulica { get; set; }
        public string Pesel { get; set; }
        public string Miejscowosc { get; set; }
        public string Wojewodztwo { get; set; }
        public Plec Plec { get; set; }


        public string RegisterResult { get; set; }
    }
}
