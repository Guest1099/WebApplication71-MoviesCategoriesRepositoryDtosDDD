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
        public DateTime DataUrodzenia { get; private set; }
        public Plec Plec { get; private set; }
        public string Telefon { get; private set; }
        public byte[] Photo { get; private set; }
        public int IloscZalogowan { get; private set; }
        public string DataOstatniegoZalogowania { get; private set; }
        public string RoleName { get; private set; }
        public DateTime DataDodania { get; private set; }




        public List<Movie>? Movies { get; private set; }
        public List<Logowanie>? Logowania { get; private set; }





        public ApplicationUser()
        {
        }


        /// <summary>
        /// Tworzenia użytkownika z bez hasła
        /// </summary>
        public ApplicationUser(string email, string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, DateTime dataUrodzenia, Plec plec, string telefon, byte[] photo, string roleName)
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
            EmailConfirmed = false;
            DataDodania = DateTime.Now;
        }


        /// <summary>
        /// Tworzenia użytkownika z hasłem
        /// </summary>
        public ApplicationUser(string email, string imie, string nazwisko, string ulica, string miejscowosc, string kodPocztowy, string wojewodztwo, string pesel, DateTime dataUrodzenia, Plec plec, string telefon, byte[] photo, string roleName, string password)
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
            EmailConfirmed = false;

            PasswordHash = PasswordHashString(password);

            DataDodania = GetDate ();
        }





        /// <summary>
        /// Tworzenia użytkownika z hasłem oraz datą dodania.
        /// Konstruktor wykorzystywany tylko i wyłącznie podczas resetowania hasła
        /// </summary>
        public ApplicationUser(string email, string imie, string nazwisko, string ulica, string miejscowosc, string kodPocztowy, string wojewodztwo, string pesel, DateTime dataUrodzenia, Plec plec, string telefon, byte[] photo, string roleName, string password, DateTime dataDodania)
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
            EmailConfirmed = false;

            PasswordHash = PasswordHashString(password);
            DataDodania = dataDodania;
        }



        public void Update(string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, DateTime dataUrodzenia, Plec plec, string telefon, byte[] photo, string roleName)
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
        public void Update(string imie, string nazwisko, string ulica, string miejscowosc, string wojewodztwo, string kodPocztowy, string pesel, DateTime dataUrodzenia, Plec plec, string telefon, byte[] photo)
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


        private DateTime GetDate ()
        {
            var d = DateTime.Now;
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }
    }
}
