using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Account;
using WebApplication71.DTOs.Users;
using WebApplication71.Models;
using WebApplication71.Services.Abs;

namespace WebApplication71.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<ResultViewModel<GetUserDto>> GetUserById(string userId)
        {
            var returnResult = new ResultViewModel<GetUserDto>() { Success = false, Message = "", Object = new GetUserDto() };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
                if (user != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = new GetUserDto()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Imie = user.Imie,
                        Nazwisko = user.Nazwisko,
                        Ulica = user.Ulica,
                        Miejscowosc = user.Miejscowosc,
                        Wojewodztwo = user.Wojewodztwo,
                        KodPocztowy = user.KodPocztowy,
                        Pesel = user.Pesel,
                        DataUrodzenia = user.DataUrodzenia,
                        Plec = user.Plec,
                        Telefon = user.Telefon,
                        Photo = user.Photo,
                        RoleName = user.RoleName,
                        DataDodania = user.DataDodania
                    };
                }
                else
                {
                    returnResult.Success = false;
                    returnResult.Message = "Wskazany adres email nie istnieje";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }
            return returnResult;
        }


        public async Task<ResultViewModel<GetUserDto>> GetUserByEmail(string email)
        {
            var returnResult = new ResultViewModel<GetUserDto>() { Success = false, Message = "", Object = new GetUserDto() };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                if (user != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = new GetUserDto()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Imie = user.Imie,
                        Nazwisko = user.Nazwisko,
                        Ulica = user.Ulica,
                        Miejscowosc = user.Miejscowosc,
                        Wojewodztwo = user.Wojewodztwo,
                        KodPocztowy = user.KodPocztowy,
                        Pesel = user.Pesel,
                        DataUrodzenia = user.DataUrodzenia,
                        Plec = user.Plec,
                        Telefon = user.Telefon,
                        Photo = user.Photo,
                        RoleName = user.RoleName,
                        DataDodania = user.DataDodania
                    };
                }
                else
                {
                    returnResult.Success = false;
                    returnResult.Message = "Wskazany adres email nie istnieje";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }
            return returnResult;
        }



        public async Task<ResultViewModel<CreateAccountDto>> CreateAccount(CreateAccountDto model)
        {
            var returnResult = new ResultViewModel<CreateAccountDto>() { Success = false, Message = "", Object = new CreateAccountDto() };

            if (model != null)
            {
                try
                {
                    // sprawdzenie czy użytkownik o wskazanym w modelu mailu nie istnieje, jeśli tak to wyświetlany jest komunikat,
                    // jeśli nie to tworzony jest nowy użytkownik

                    if ((await _userManager.FindByEmailAsync(model.Email)) == null)
                    {

                        var user = new ApplicationUser(
                            email: model.Email,
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            ulica: model.Ulica,
                            miejscowosc: model.Miejscowosc,
                            wojewodztwo: model.Wojewodztwo,
                            kodPocztowy: model.KodPocztowy,
                            pesel: model.Pesel,
                            dataUrodzenia: model.DataUrodzenia,
                            plec: model.Plec,
                            telefon: model.Telefon,
                            photo: model.Photo,
                            roleName: model.RoleName,
                            password: model.Password
                            );

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            // przypisanie roli użytkownikowi
                            await _userManager.AddToRoleAsync(user, model.RoleName);


                            returnResult.Success = true;
                            returnResult.Message = "Zarejestrowano nowego użytkownika";
                            returnResult.Object = model;
                        }
                        else
                        {
                            returnResult.Message = "Nie można zarejestrować użytkownika";
                        }
                    }
                    else
                    {
                        returnResult.Message = "Wskazany adres email już istnieje";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }










        public async Task<ResultViewModel<UpdateAccountDto>> UpdateAccount(UpdateAccountDto model)
        {
            var returnResult = new ResultViewModel<UpdateAccountDto>() { Success = false, Message = "", Object = new UpdateAccountDto() };

            if (model != null)
            {
                try
                {
                    ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Id == model.Id);
                    if (user != null)
                    {
                        user.Update(
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            ulica: model.Ulica,
                            miejscowosc: model.Miejscowosc,
                            wojewodztwo: model.Wojewodztwo,
                            kodPocztowy: model.KodPocztowy,
                            pesel: model.Pesel,
                            dataUrodzenia: model.DataUrodzenia,
                            plec: model.Plec,
                            telefon: model.Telefon,
                            photo: await ChangeFileToBytes (model.PhotoData)
                            );


                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            // dodanie zdjęcia
                            //await CreateNewPhoto (model.Files, user.Id);

/*
                            // usunięcie użytkownika z rół i przypisanie na nowo
                            var userRoles = await _userManager.GetRolesAsync(user);
                            await _userManager.RemoveFromRolesAsync(user, userRoles);

                            // przypisanie użytkownika do nowej roli
                            await _userManager.AddToRoleAsync(user, model.RoleName);
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.RoleName));
*/



                            returnResult.Success = true;
                            returnResult.Message = "Dane zostały zaktualizowane poprawnie";
                            returnResult.Object = model;

                        }
                        else
                        {
                            returnResult.Message = "Dane nie zostały zaktualizowane";
                        }
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }








        public async Task<ResultViewModel<bool>> DeleteAccountByEmail(string email)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                    if (user != null)
                    {
                        _context.Users.Remove(user);
                        await _context.SaveChangesAsync();


                        returnResult.Success = true;
                        returnResult.Object = true;
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }





        public async Task<ResultViewModel<ChangeEmailDto>> ChangeEmail(ChangeEmailDto model)
        {
            var returnResult = new ResultViewModel<ChangeEmailDto>() { Success = false, Message = "", Object = new ChangeEmailDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza na podstawie nowego maila czy użytkownik już istnieje, jeśli tak zwraca informację
                    // jeśli nie aktualizuje maila
                    if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.NewEmail)) == null)
                    {
                        ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            string token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
                            if (result.Succeeded)
                            {
                                // zaktualizowanie emaila oraz nazwy użytkownika 
                                user.UpdateEmail(
                                    email: model.NewEmail
                                    );
                                await _userManager.UpdateAsync(user);

                                //await _signInManager.SignOutAsync ();
                                returnResult.Success = true;
                                returnResult.Message = "Email został zmieniony poprawnie";
                                returnResult.Object = model;
                            }
                            else
                            {
                                returnResult.Message = "Email nie został zmieniony";
                            }
                        }
                        else
                        {
                            returnResult.Message = "User was null";
                        }
                    }
                    else
                    {
                        returnResult.Message = "Użytkownik o takim adresie email już istnieje";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }

            return returnResult;
        }





        public async Task<ResultViewModel<ChangePasswordDto>> ChangePassword(ChangePasswordDto model)
        {
            var returnResult = new ResultViewModel<ChangePasswordDto>() { Success = false, Message = "", Object = new ChangePasswordDto() };

            if (model != null)
            {
                try
                {
                    ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                    if (user != null)
                    {
                        IdentityResult result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                        if (result.Succeeded)
                        {
                            returnResult.Success = true;
                            returnResult.Message = "Hasło zostało zmienione poprawnie";
                            returnResult.Object = model;

                            // wylogowanie
                            //await _signInManager.SignOutAsync ();
                        }
                        else
                        {
                            returnResult.Message = "Błędne hasło";
                        }
                    }
                    else
                    {
                        returnResult.Message = "Wskazany użytkownik nie istnieje";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }









        /// <summary>
        /// Pobiera wszystkie role danego użytkownika
        /// </summary>
        public async Task<ResultViewModel<List<string>>> GetUserRoles(string email)
        {
            var returnResult = new ResultViewModel<List<string>>() { Success = false, Message = "", Object = new List<string>() };

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                    if (user != null)
                    {
                        var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

                        returnResult.Success = true;
                        returnResult.Object = userRoles;
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }





        /// <summary>
        /// Pobiera wszystkich użytkowników będących w danej roli
        /// </summary>
        public async Task<ResultViewModel<List<ApplicationUser>>> GetUsersInRole(string roleName)
        {
            var returnResult = new ResultViewModel<List<ApplicationUser>>() { Success = false, Message = "", Object = new List<ApplicationUser>() };

            if (!string.IsNullOrEmpty(roleName))
            {

                try
                {
                    var usersInRole = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
                    if (usersInRole != null)
                    {
                        returnResult.Success = true;
                        returnResult.Object = usersInRole;
                    }
                    else
                    {
                        returnResult.Success = false;
                        returnResult.Message = $"usersInRole was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }

            return returnResult;
        }




        /// <summary>
        /// Sprawdza czy użytkownik jest w danej roli
        /// </summary>
        public async Task<ResultViewModel<bool>> UserInRole(string email, string roleName)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(roleName))
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        var userInRole = await _userManager.IsInRoleAsync(user, roleName);
                        if (userInRole)
                        {
                            returnResult.Success = true;
                            returnResult.Object = userInRole;
                        }
                        else
                        {
                            returnResult.Message = "userInRole was null";
                        }
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }

                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Email or role name was null";
            }

            return returnResult;
        }





        /// <summary>
        /// Sprawdza czy zalogowany user jest administratorem, jeśli tak to przekierowuje go do panelu administratora
        /// </summary>
        public async Task<ResultViewModel<bool>> LoggedUserIsAdmin(string email)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                    if (user != null)
                    {
                        bool isInRole = await _userManager.IsInRoleAsync(user, "Administrator");
                        returnResult.Success = isInRole;
                        returnResult.Object = isInRole;
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Email was null";
            }

            return returnResult;
        }





        public async Task<ResultViewModel<LoginDto>> Login(LoginDto model)
        {
            var returnResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto() };

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // mówi o tym, kiedyu żytkownik się zalogował 
                        Logowanie logowanie = new Logowanie(
                            userId: user.Id
                            );
                        _context.Logowania.Add(logowanie);
                        await _context.SaveChangesAsync();


                        returnResult.Success = true;
                        returnResult.Object = model;
                    }
                    else
                    {
                        returnResult.Message = "Błędny login lub hasło";
                    }
                }
                else
                {
                    returnResult.Message = "Użytkownik nie istnieje";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }

            return returnResult;
        }





        /*
                public Task<TaskResult<string>> GenerateNewToken(TokenViewModel model)
                {
                    var taskResult = new TaskResult<string>() { Success = true, Model = "", Message = "" };

                    try
                    {
                        if (model.Email != null && model.Role != null)
                        {
                            var storedRefreshToken = _userSupportService.GenerateJwtNewToken(model.Email, model.Role);
                            if (!string.IsNullOrEmpty(storedRefreshToken))
                            {
                                taskResult.Model = storedRefreshToken;
                                taskResult.Success = true;
                            }
                            else
                            {
                                taskResult.Success = false;
                                taskResult.Message = "Nie można było wygenerować nowego tokena";
                            }
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Email or role was null";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return Task.FromResult(taskResult);
                }
        */


        /*
                public Task<TaskResult<bool>> TokenTimeExpired ()
                {
                    var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

                    try
                    {
                        if (DateTime.Now >= czasWylogowania)
                        {
                            taskResult.Success = true;
                            taskResult.Model = true;
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Model = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return Task.FromResult (taskResult);
                }
        */




        /*
                public async Task Logout(string userEmail)
                {
                    // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
                    var ostatnieLogowanieUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .OrderByDescending(o => o.DataLogowania)
                        .FirstOrDefaultAsync(f => f.User.Email == userEmail);

                    if (ostatnieLogowanieUzytkownika != null)
                    {
                        // zapisanie w bazie daty wylogowania użytkownika
                        ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                        _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }








                    var wszystkieRekordy = await _context.Logowania.ToListAsync();

                    var zsumowanyCzas = wszystkieRekordy
                        .GroupBy(w => DateTime.Parse(w.DataLogowania).Date)
                        .Select(g => new
                        {
                            Data = g.Key,
                            CzasPracyMinut = g.Sum(x =>
                            {
                                var logowanie = DateTime.Parse(x.DataLogowania);
                                var wylogowanie = string.IsNullOrEmpty(x.DataWylogowania)
                                    ? DateTime.Now // Używamy bieżącego czasu, jeśli brak daty wylogowania
                                    : DateTime.Parse(x.DataWylogowania);

                                return (wylogowanie - logowanie).TotalMinutes;
                            }),
                            PierwszeLogowanie = g.First().DataLogowania, // Pierwsze logowanie w grupie
                            OstatnieWylogowanie = g.Last().DataWylogowania, // Ostatnie wylogowanie w grupie
                            UserId = g.First().User.Id // Id użytkownika
                        })
                        .ToList();

                    // usuniecie wszystkich pozostałych rekordów i zapisanie nowych
                    *//*foreach (var w in wszystkieRekordy)
                    {
                        _context.Logowania.Remove (w);
                        await _context.SaveChangesAsync ();
                    }*//*

                    // Dodawanie zsumowanego czasu pracy jako nowych rekordów
                    foreach (var dzien in zsumowanyCzas)
                    {
                        var logowanie = new Logowanie(
                            dzien.PierwszeLogowanie, // Przyjmujemy pierwszy czas logowania + "_XXX"
                            dzien.OstatnieWylogowanie, // Przyjmujemy ostatni czas wylogowania
                            dzien.CzasPracyMinut.ToString() + "_XXX", // Całkowity czas pracy w minutach
                            dzien.UserId // Id użytkownika
                        );

                        _context.Logowania.Add(logowanie);
                    }

                    await _context.SaveChangesAsync(); // Zapisujemy zmiany






                    // wylogowanie
                    await _signInManager.SignOutAsync();
                }
        */




        public async Task Logout(string email)
        {
            try
            {
                
                // wyszukuje najnowszy rekord logowania oraz dopisuje do niego datę wylogowania
                var ostatnieLogowanieUzytkownika = await _context.Logowania
                    .Include(i => i.User)
                    .OrderByDescending(o => o.DataLogowania)
                    .FirstOrDefaultAsync(f => f.User.Email == email);

                if (ostatnieLogowanieUzytkownika != null)
                {
                    // zapisanie w bazie daty wylogowania użytkownika
                    ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now);
                    _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }




                // usówa rekordy w których DataWylogowania jest pustym polem
                var znajdzWszystkieZalogowaniaUzytkownika = await _context.Logowania 
                    .Include (i=> i.User)
                    .ToListAsync ();

                foreach (var log in znajdzWszystkieZalogowaniaUzytkownika)
                {
                    DateTime dataZalogowania = log.DataLogowania;

                    /*if (DateTime.Now.AddDays(-2) >= dataZalogowania)
                    {
                        if (string.IsNullOrEmpty(log.DataWylogowania))
                        {
                            _context.Logowania.Remove (log);
                            await _context.SaveChangesAsync();
                        }
                    }*/
                }


/*
                // jeżeli jakiś rekord przypisany do tego użytkownika w polu DataWylogowania ma puste pole wtedy taki rekord jest usuwany
                var logowaniaUzytkownika = await _context.Logowania
                    .Include(i => i.User)
                    .OrderByDescending(o => o.DataLogowania)
                    .Where(w => w.User.Email == email)
                    .ToListAsync();

                foreach (var logowanie in logowaniaUzytkownika)
                {
                    if (!string.IsNullOrEmpty(logowanie.DataWylogowania))
                    {
                        _context.Logowania.Remove(logowanie);
                        await _context.SaveChangesAsync();
                    }
                }
*/


                // wylogowanie
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {

            }
        }







        private async Task CreateNewPhoto(List<IFormFile> files, string userId)
        {
            /*try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte [] photoData;
                            using (var stream = new MemoryStream ())
                            {
                                file.CopyTo (stream);
                                photoData = stream.ToArray ();

                                PhotoUser photoUser = new PhotoUser ()
                                {
                                    PhotoUserId = Guid.NewGuid ().ToString (),
                                    PhotoData = photoData,
                                    UserId = userId
                                };
                                _context.PhotosUser.Add (photoUser);
                                await _context.SaveChangesAsync ();
                            }
                        }
                    }
                }
            }
            catch { }*/
        }






        /// <summary>
        /// Zamienia zdjęcie na bytes
        /// </summary>
        private async Task<byte[]> ChangeFileToBytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                byte[] photoData;
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    photoData = stream.ToArray();
                }

                return photoData;
            }
            catch
            {
                return null;
            }
        }




    }
}
