using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using WebApplication71.Models.Enums;

namespace WebApplication71.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        public string Ulica { get; private set; }
        public string Miejscowosc { get; private set; }
        public string KodPocztowy { get; private set; }
        public string Wojewodztwo { get; private set; }
        public string Pesel { get; private set; }
        public string DataUrodzenia { get; private set; }
        public Plec Plec { get; private set; }
        public string Telefon { get; private set; }
        public byte [] Photo { get; private set; }
        public int IloscZalogowan { get; private set; }
        public string DataOstatniegoZalogowania { get; private set; }
        public string RoleName { get; private set; }
        public string DataDodania { get; private set; }




        public List<Movie>? Movies { get; private set; }
        public List<Logowanie>? Logowania { get; private set; }





        public ApplicationUser()
        {
        }


        /// <summary>
        /// Tworzenia użytkownika z bez hasła
        /// </summary>
        public ApplicationUser(string email, string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, string dataUrodzenia, Plec plec, string telefon, byte [] photo, string roleName)
        {
            Id = Guid.NewGuid().ToString();

            Email = email;
            UserName = email; // nazwa użytkowika taka sama jak email
            if (!string.IsNullOrEmpty (email))
            {
                NormalizedUserName = email.ToUpper();
                NormalizedEmail = email.ToUpper();
            }
            SecurityStamp = Guid.NewGuid().ToString();

            Imie = imie;
            Nazwisko = nazwisko;
            Ulica = ulica;
            Miejscowosc = miejscowosc;
            Wojewodztwo = wojewodztwo;
            KodPocztowy = kodPocztowy;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
            Plec = plec;
            Telefon = telefon;
            Photo = photo;
            IloscZalogowan = 0;
            DataOstatniegoZalogowania = "";
            RoleName = roleName;
            DataDodania = DateTime.Now.ToString();
        }


        /// <summary>
        /// Tworzenia użytkownika z hasłem
        /// </summary>
        public ApplicationUser(string email, string imie, string nazwisko, string ulica, string miejscowosc, string kodPocztowy, string wojewodztwo, string pesel, string dataUrodzenia, Plec plec, string telefon, byte [] photo, string roleName, string password)
        {
            Id = Guid.NewGuid().ToString();

            Email = email;
            UserName = email; // nazwa użytkowika taka sama jak email
            if (!string.IsNullOrEmpty(email))
            {
                NormalizedUserName = email.ToUpper();
                NormalizedEmail = email.ToUpper();
            }
            SecurityStamp = Guid.NewGuid().ToString();

            Imie = imie;
            Nazwisko = nazwisko;
            Ulica = ulica;
            Miejscowosc = miejscowosc;
            Wojewodztwo = wojewodztwo;
            KodPocztowy = kodPocztowy;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
            Plec = plec;
            Telefon = telefon;
            Photo = photo;
            IloscZalogowan = 0;
            DataOstatniegoZalogowania = "";
            RoleName = roleName;

            PasswordHash = PasswordHashString(password);

            DataDodania = DateTime.Now.ToString();
        }





        public void Update(string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, string dataUrodzenia, Plec plec, string telefon, byte[] photo, string roleName)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Ulica = ulica;
            Miejscowosc = miejscowosc;
            Wojewodztwo = wojewodztwo;
            KodPocztowy = kodPocztowy;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
            Plec = plec;
            Telefon = telefon;
            Photo = photo;
            RoleName = roleName;
        }


        /// <summary>
        /// Metoda do aktualizacji danych zalogowanego użytkownika, bez aktualizacji jego roli
        /// </summary>
        public void Update(string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, string dataUrodzenia, Plec plec, string telefon, byte[] photo)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Ulica = ulica;
            Miejscowosc = miejscowosc;
            Wojewodztwo = wojewodztwo;
            KodPocztowy = kodPocztowy;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
            Plec = plec;
            Telefon = telefon;
            Photo = photo;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
            UserName = email;
            NormalizedUserName = email.ToUpper();
            NormalizedEmail = email.ToUpper();
        }


        public void IloscZalogowanUpdate(int iloscZalogowan)
        {
            IloscZalogowan = iloscZalogowan;
        }


        private string PasswordHashString(string haslo)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            return passwordHasher.HashPassword(this, haslo);
        }
    }
}
