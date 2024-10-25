using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.DTOs.Users
{
    public class EditUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }




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
        public string Pesel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }

        [Required]
        public Plec Plec { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public string Photo { get; set; }
    }
}
