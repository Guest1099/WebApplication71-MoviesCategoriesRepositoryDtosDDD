using Application.Services.Abs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication71.Controllers;
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
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
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

                            user.EmailConfirmed = false;
                            user.LockoutEnabled = false;
                            user.SecurityStamp = Guid.NewGuid().ToString();
                            await _userManager.UpdateAsync(user);


                            // przypisanie roli użytkownikowi
                            await _userManager.AddToRoleAsync(user, model.RoleName);
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.RoleName));


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
                        object photoData = model.PhotoData == null ? user.Photo : await ChangeFileToBytes(model.PhotoData);
                        byte[] photo = photoData as byte[];

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
                            photo: photo
                            );


                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {

                            user.EmailConfirmed = false;
                            user.LockoutEnabled = false;
                            user.SecurityStamp = Guid.NewGuid().ToString();
                            await _userManager.UpdateAsync(user);


                            // dodanie zdjęcia
                            //await CreateNewPhoto (model.Files, user.Id);



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

                                user.EmailConfirmed = false;
                                user.LockoutEnabled = false;
                                user.SecurityStamp = Guid.NewGuid().ToString();
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





        public async Task<ResultViewModel<ForgotPasswordDto>> ForgotPassword(ForgotPasswordDto model)
        {
            var returnResult = new ResultViewModel<ForgotPasswordDto>() { Success = false, Message = "", Object = new ForgotPasswordDto() };

            if (model != null)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                    if (user == null /*|| !(await _userManager.IsEmailConfirmedAsync(user))*/)
                    {
                        returnResult.Message = "Wskazany użytkownik nie istnieje";
                    }
                    else
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        /*
                                                string url = GenerateResetPasswordUrl(token, model.Email);
                                                string sendMessage = $"Proszę zresetować swoje hasło, klikając tutaj: <a href='{url}'>link</a>";
                                                _emailSender.SendEmail(model.Email, "Resetowanie hasła", sendMessage);
                        */

                        string url = GenerateResetPasswordUrl(token, model.Email); // dla jakiego maila generowany jest token
                        string sendMessage = $"Proszę zresetować swoje hasło, klikając tutaj {model.Email}: <a href='{url}'>link</a>";
                        _emailSender.SendEmail(model.Email, "Resetowanie hasła", sendMessage); // pod jaki adres wysyłany jest email


                        //_emailSender.SendEmail("mgmcdeveloper@gmail.com"); // email do testowania czy działa wysyłka

                        returnResult.Success = true;
                        returnResult.Message = "Na twojego maila wysłany został link z możliwością zresetowania hasła";
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



        public string GenerateResetPasswordUrl(string token, string email)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var urlHelper = _urlHelperFactory.GetUrlHelper(new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor()));

            return urlHelper.Action("ResetPassword", "Account", new { token = token, email = email }, httpContext.Request.Scheme);
        }



        public async Task<ResultViewModel<ResetPasswordDto>> ResetPassword(ResetPasswordDto model)
        {
            var returnResult = new ResultViewModel<ResetPasswordDto>() { Success = false, Message = "", Object = new ResetPasswordDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza czy hasła się różnią
                    if (model.Password == model.ConfirmPassword)
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                        if (user != null)
                        {
                            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                            if (result.Succeeded)
                            {
                                returnResult.Success = true;
                                returnResult.Message = "Hasło zostało zresetowane";
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
                    else
                    {
                        returnResult.Message = "Hasła w polach muszą być identyczne";
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






        /*


        // W tej akcji wysyłasz e-mail z linkiem potwierdzającym, zawierającym token.
        public async Task<SendConfirmationViewModel> SendConfirmationEmail(SendConfirmationViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Email);
            if (user != null)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                *//*var callbackUrl = Url.Action("ConfirmEmail", "Account",
        new { userId = user.Id, code = encodedToken }, protocol: HttpContext.Request.Scheme);*//*

                // Tutaj wyślij e-mail z linkiem potwierdzającym, zawierającym callbackUrl

                model.UserId = user.Id;
                model.Code = encodedToken;
            }
            return model;
        }


        public async Task<ConfirmEmailViewModel> ConfirmEmail(ConfirmEmailViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId) || !string.IsNullOrEmpty(model.Email))
                model.Result = false;

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                string code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    model.Code = code;
                    model.Result = true;
                }
            }
            return model;
        }
*/





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




        /*
                public async Task<ResultViewModel<LoginDto>> Login(LoginDto model)
                {
                    var returnResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto() };

                    try
                    {
                        var user = await _context.Users.FirstOrDefaultAsync (f=> f.Email == model.Email);
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
                                returnResult.Message = "Błędny login lub hasło"; // tu jest błędne hasło
                            }
                        }
                        else
                        {
                            returnResult.Message = "Błędny login lub hasło"; // tu jest błędny login
                        }
                    }
                    catch (Exception ex)
                    {
                        returnResult.Message = $"Exception: {ex.Message}";
                    }

                    return returnResult;
                }
        */




        /// <summary>
        /// Logowanie z 3 próbami zalogowania, po czym konto jest blokowane na 6 godzin
        /// </summary>
        public async Task<ResultViewModel<LoginDto>> Login(LoginDto model)
        {
            var returnResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto() };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                if (user != null)
                {
                    // sprawdza czy konto nie jest zablokowane, jeśli nie to następuje zalogowanie do systemu, jeśli tak to wyświetlana jest informacja, że user ma zalobkowane konto i po 6 godzinach zostanie odblokowane podczas próby pierwszego poprawnego logowania

                    // jeżeli ilość ilość logowań jset mniejsza niż 3 użytkownik może się zalogować do systemu

                    // jeżeli konto jest zablokowane, wyświetl komunikat
                    if (user.LockoutEnabled)
                    {
                        // jeżeli użytkownik nie ma zablokowanego konta, a źle się zalogował i zaniechał dalszych prób logowania do konta to po
                        // upłynięciu 6 godzin czasu data błędnego zalogowania oraz ilość zalogowanych prób jest czyszczona
                        if (!string.IsNullOrEmpty(user.DataZablokowaniaKonta))
                        {
                            DateTime dataZablokowaniaKonta = DateTime.Parse(user.DataZablokowaniaKonta);
                            DateTime now = DateTime.Now;

                            // jeżeli data zablokowania przeminęła to zerowany jest licznik nieudanyhc logowań oraz data nieudanego logowania
                            if (dataZablokowaniaKonta < now)
                            {
                                // wyczyść daty i liczniki z nieudanymi próbami logowania
                                if (user.IloscLogowan == 3)
                                {
                                    user.IloscLogowan = 0;
                                    await _userManager.UpdateAsync(user);
                                }

                                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                                if (result.Succeeded)
                                {
                                    // mówi o tym, kiedyu żytkownik się zalogował 
                                    Logowanie logowanie = new Logowanie(
                                        userId: user.Id
                                        );
                                    _context.Logowania.Add(logowanie);
                                    await _context.SaveChangesAsync();


                                    // zerowanie licznika nieudanych prób logowania 
                                    user.IloscLogowan = 0;
                                    user.DataZablokowaniaKonta = "";
                                    user.LockoutEnabled = false;
                                    await _userManager.UpdateAsync(user);


                                    //await SprawdzCzyZostalaDopisanaDataWylogowania (user);


                                    returnResult.Success = true;
                                    returnResult.Object = model;
                                }
                                else
                                {
                                    // aktualizacja ilości logowań
                                    user.IloscLogowan = user.IloscLogowan + 1;
                                    await _userManager.UpdateAsync(user);

                                    int iloscLogowan = 3 - user.IloscLogowan;
                                    returnResult.Message = $"Błędne hasło. Pozostały {iloscLogowan} próby logowań";


                                    // jeżeli ilość prób logowań wyniesie 3 wtedy zablokuj konto
                                    if (user.IloscLogowan == 3)
                                    {
                                        //user.LockoutEnabled = true;
                                        user.DataZablokowaniaKonta = DateTime.Now.AddSeconds(10).ToString();
                                        await _userManager.UpdateAsync(user);
                                        returnResult.Message = "Konto użytkownika zostało zablokowane, spróbuj się zalogować za jakiś czas";
                                    }
                                }
                            }
                            else
                            {
                                returnResult.Message = "Konto użytkownika zostało zablokowane, spróbuj się zalogować za jakiś czas";
                            }
                        }
                    }
                    // jeżeli nie spróbuj zalogować
                    else
                    {
                        // jeżeli użytkownik nie ma zablokowanego konta, a źle się zalogował i zaniechał dalszych prób logowania do konta to po
                        // upłynięciu 6 godzin czasu data błędnego zalogowania oraz ilość zalogowanych prób jest czyszczona
                        if (!string.IsNullOrEmpty(user.DataZablokowaniaKonta))
                        {
                            DateTime dataZablokowaniaKonta = DateTime.Parse(user.DataZablokowaniaKonta);
                            DateTime now = DateTime.Now;

                            // jeżeli data zablokowania przeminęła to zerowany jest licznik nieudanyhc logowań oraz data nieudanego logowania
                            if (dataZablokowaniaKonta < now)
                            {
                                // wyczyść daty i liczniki z nieudanymi próbami logowania
                                user.IloscLogowan = 0;
                                user.DataZablokowaniaKonta = "";
                                await _userManager.UpdateAsync(user);

                                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                                if (result.Succeeded)
                                {
                                    // mówi o tym, kiedyu żytkownik się zalogował 
                                    Logowanie logowanie = new Logowanie(
                                        userId: user.Id
                                        );
                                    _context.Logowania.Add(logowanie);
                                    await _context.SaveChangesAsync();


                                    // zerowanie licznika nieudanych prób logowania 
                                    user.IloscLogowan = 0;
                                    user.DataZablokowaniaKonta = "";
                                    user.LockoutEnabled = false;
                                    await _userManager.UpdateAsync(user);



                                    //await SprawdzCzyZostalaDopisanaDataWylogowania(user);


                                    returnResult.Success = true;
                                    returnResult.Object = model;
                                }
                                else
                                {
                                    // aktualizacja ilości logowań
                                    user.IloscLogowan = user.IloscLogowan + 1;
                                    user.DataZablokowaniaKonta = DateTime.Now.AddSeconds(10).ToString();
                                    await _userManager.UpdateAsync(user);

                                    int iloscLogowan = 3 - user.IloscLogowan;
                                    returnResult.Message = $"Błędne hasło. Pozostały {iloscLogowan} próby logowań";


                                    // jeżeli ilość prób logowań wyniesie 3 wtedy zablokuj konto
                                    if (user.IloscLogowan == 3)
                                    {
                                        user.LockoutEnabled = true;
                                        await _userManager.UpdateAsync(user);
                                        returnResult.Message = "Konto użytkownika zostało zablokowane, spróbuj się zalogować za jakiś czas";
                                    }
                                }
                            }
                            // jeśli data zablokowania konta nie przeminęła dalej podbijany jest licznik nieudanych prób, i leżeli osiągnie on maximum, czyli 3 wtedy konto jest blokowane na 6 godzin
                            else
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


                                    // zerowanie licznika nieudanych prób logowania 
                                    user.IloscLogowan = 0;
                                    user.DataZablokowaniaKonta = "";
                                    user.LockoutEnabled = false;
                                    await _userManager.UpdateAsync(user);


                                    //await SprawdzCzyZostalaDopisanaDataWylogowania(user);


                                    returnResult.Success = true;
                                    returnResult.Object = model;
                                }
                                else
                                {
                                    // aktualizacja ilości logowań
                                    user.IloscLogowan = user.IloscLogowan + 1;
                                    await _userManager.UpdateAsync(user);

                                    int iloscLogowan = 3 - user.IloscLogowan;
                                    returnResult.Message = $"Błędne hasło. Pozostały {iloscLogowan} próby logowań";


                                    // jeżeli ilość prób logowań wyniesie 3 wtedy zablokuj konto
                                    if (user.IloscLogowan == 3)
                                    {
                                        user.LockoutEnabled = true;
                                        user.DataZablokowaniaKonta = DateTime.Now.AddSeconds(10).ToString();
                                        await _userManager.UpdateAsync(user);
                                        returnResult.Message = "Konto użytkownika zostało zablokowane, spróbuj się zalogować za jakiś czas";
                                    }
                                }
                            }
                        }
                        else
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


                                // zerowanie licznika nieudanych prób logowania 
                                user.IloscLogowan = 0;
                                user.DataZablokowaniaKonta = "";
                                user.LockoutEnabled = false;
                                await _userManager.UpdateAsync(user);


                                //await SprawdzCzyZostalaDopisanaDataWylogowania(user);


                                returnResult.Success = true;
                                returnResult.Object = model;
                            }
                            else
                            {
                                // aktualizacja ilości logowań
                                user.IloscLogowan = user.IloscLogowan + 1;
                                user.DataZablokowaniaKonta = DateTime.Now.AddSeconds(10).ToString();
                                await _userManager.UpdateAsync(user);

                                int iloscLogowan = 3 - user.IloscLogowan;
                                returnResult.Message = $"Błędne hasło. Pozostały {iloscLogowan} próby logowań";


                                // jeżeli ilość prób logowań wyniesie 3 wtedy zablokuj konto
                                if (user.IloscLogowan == 3)
                                {
                                    user.LockoutEnabled = true;
                                    await _userManager.UpdateAsync(user);
                                    returnResult.Message = "Konto użytkownika zostało zablokowane, spróbuj się zalogować za jakiś czas";
                                }
                            }
                        }

                    }
                }
                else
                {
                    returnResult.Message = "Błędny login lub hasło"; // tu jest błędny login
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }

            return returnResult;
        }

        private async Task<ResultViewModel<LoginDto>> LoginInternal(ApplicationUser user, LoginDto model)
        {
            var returnResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto() };

            // jeżeli ilość ilość logowań jset mniejsza niż 3 użytkownik może się zalogować do systemu
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // mówi o tym, kiedyu żytkownik się zalogował 
                Logowanie logowanie = new Logowanie(
                    userId: user.Id
                    );
                _context.Logowania.Add(logowanie);
                await _context.SaveChangesAsync();


                // zerowanie licznika nieudanych prób logowania
                user.IloscLogowan = 0;
                user.DataZablokowaniaKonta = "";
                await _userManager.UpdateAsync(user);


                returnResult.Success = true;
                returnResult.Object = model;
            }
            else
            {
                /*
                                // sprawdź ile razy użytkownik już się logował, ma 3 próby, po 3 niudanej próbie logowania konto jest blokonwane na 6 godzin                                
                                int iloePozostaloLogowanUzytkownikowi = 2 - user.IloscLogowan;
                                if (iloePozostaloLogowanUzytkownikowi == 0)
                                {
                                    returnResult.Message = $"Konto użytkownika zostało zablokowane, spróbuj ponownie za jakiś czas";
                                }
                                else
                                {
                                    returnResult.Message = $"Błędny login lub hasło. Pozostały Ci {iloePozostaloLogowanUzytkownikowi} próby logowania";
                                }
                */

                // aktualizacja ilości logowań
                user.IloscLogowan = user.IloscLogowan + 1;
                user.DataZablokowaniaKonta = DateTime.Now.AddMinutes(1).ToString();
                await _userManager.UpdateAsync(user);

                if (user.IloscLogowan == 3)
                {
                    user.LockoutEnabled = true;
                    await _userManager.UpdateAsync(user);
                }
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

         


        public async Task Logout(string email)
        {
            try
            { 

                await AktualizacjaRekorduLogowania (email);



                /*
                                // USUWANIE ZBĘDNYCH REKORDÓW

                                // znalezienie wszystkich rekoród posiadających "01.01.0001 00:00:00" w DataWylogowania
                                var logowania = await _context.Logowania
                                    .Include (i=> i.User)
                                    .Where (w=> w.DataWylogowania == DateTime.Parse ("01.01.0001 00:00:00"))
                                    .ToListAsync ();


                                Dictionary <string, List<Logowanie>> results = new Dictionary<string, List<Logowanie>> ();
                                // najpierwsz pętla przechodzi przez wszystkich użytkowników aby pobrać "01.01.0001 00:00:00" dla indywidualnych użytkowników
                                foreach (var user in _context.Users.ToList())
                                {
                                    // pętla przez wszystkie powyższe logowania
                                    foreach (var logowanie in logowania)
                                    {
                                        if (logowanie.UserId == user.Id)
                                        {
                                            List <Logowanie> logowaniaUzytkownika = new List<Logowanie> ();
                                            // jeżeli użytkownik jest już w bazie dodaj do niego kolejny rekord z jego zalogowaniem
                                            if (results.ContainsKey(user.Email))
                                            {
                                                logowaniaUzytkownika = results[user.Email];
                                                logowaniaUzytkownika.Add (logowanie);
                                            }
                                            else
                                            {
                                                logowaniaUzytkownika.Clear ();
                                                logowaniaUzytkownika.Add (logowanie);
                                                results.Add (user.Email, logowaniaUzytkownika);
                                            }
                                        }
                                    }
                                }
                                // usunięcie rekordów logowania według elementeów znajdujących się w słowniku
                                foreach (var result in results)
                                {
                                    result.Value.OrderByDescending (o=> o.DataLogowania); // posortowanie elementów wg. daty logowania
                                    int iloscElementow = result.Value.Count;
                                    var lastElementLogowanie = result.Value.Last();


                                    var now = DateTime.Now.AddDays(-2); // usinięcie rekordów sprzed dwóch dni

                                    if (lastElementLogowanie.DataLogowania < now)
                                    {
                                        // usunięcie logowania z bazy
                                        _context.Logowania.Remove(lastElementLogowanie);
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


        private async Task AktualizacjaRekorduLogowania (string email)
        {
            await ZaktualizujRekordLogowaniaDopisujacDoNiegoGodzineWylogowania(email);


            var zalogowanyUser = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
            if (zalogowanyUser != null)
            {
                switch (zalogowanyUser.RoleName)
                {
                    case "Administrator":

                        // operacja odbywa się wyłącznie przez administratora dla wszystkich użytkowników
                        await SprawdzCzyZostalaDopisanaDataWylogowania();

                        break;

                    case "User":
                        // operacja odbywa się przez zalogowanego użytkownika dla zalogowanego użytkownika
                        //await SprawdzCzyZostalaDopisanaDataWylogowania(email);
                        break;
                }
            }
        }


        /// <summary>
        ///  Operacja jeszcze nie używana
        /// </summary>
        private async Task ZaktualizujRekordLogowaniaDopisujacDoNiegoGodzineWylogowania (string email)
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
        }



        /*
                /// <summary>
                /// Przy zalogowaniu sprawdza czy do poprzedniego wylogowania została dopisana data wylogowania, jeśli nie to dopisuje
                /// </summary>
                private async Task SprawdzCzyZostalaDopisanaDataWylogowania(ApplicationUser user)
                {
                    // pobiera logowania zalogowanego użytkownika bez dopisanej daty wylogowania
                    var logowaniaUzytkownika = await _context.Logowania
                        .Where(w => w.UserId == user.Id && w.DataWylogowania == DateTime.Parse("01.01.0001 00:00:00"))
                        .OrderByDescending(o => o.DataLogowania)
                        .ToListAsync();

                    if (logowaniaUzytkownika.Count > 1)
                    {
                        foreach (var logowanieUzytkownika in logowaniaUzytkownika)
                        {
                            logowanieUzytkownika.DodajDateWylogowania(
                                dataWylogowania: DateTime.Now
                                );

                            _context.Entry(logowanieUzytkownika).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
        */



        /// <summary>
        /// Przy zalogowaniu sprawdza czy do poprzedniego wylogowania została dopisana data wylogowania, jeśli nie to dopisuje
        /// Operacja odbywa się dla wszystkich użytkowników
        /// </summary>
        private async Task SprawdzCzyZostalaDopisanaDataWylogowania()
        {
            foreach (var user in await _context.Users.ToListAsync())
            {
                foreach (var logowanie in await _context.Logowania.ToListAsync())
                {

                    var logowaniaUzytkownika = await _context.Logowania.Where(w => w.UserId == user.Id).OrderByDescending(o => o.DataLogowania).ToListAsync(); // pobranie wszytkich logowań dla danego użytkownika
                    List<Logowanie> logowaniaUzytkownikaKopia = logowaniaUzytkownika;
                    if (logowaniaUzytkownikaKopia.Count > 1)
                    {
                        logowaniaUzytkownikaKopia.RemoveAt(0); // usunięcie pierwszego rekordu i zajęcie się pozostałymi rekordami, ponieważ pierwszy rekord należy do zalogowanego użytkownika
                        foreach (var logowanieUzytkownikaKopia in logowaniaUzytkownikaKopia)
                        {
                            if (logowanieUzytkownikaKopia.DataWylogowania == DateTime.Parse("01.01.0001 00:00:00"))
                            {

                                var dz = logowanieUzytkownikaKopia.DataLogowania;
                                var dw = DateTime.Now;
                                var cp = dw - dz;
                                TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
                                logowanieUzytkownikaKopia.DataWylogowania = DateTime.Now;
                                logowanieUzytkownikaKopia.CzasPracy = czasPracy;

                                _context.Entry(logowanie).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }

                        }
                    }
                }
            }

        }



        /*
                /// <summary>
                /// Przy zalogowaniu sprawdza czy do poprzedniego wylogowania została dopisana data wylogowania, jeśli nie to dopisuje
                /// Operacja odbywa się dla wszystkich użytkowników
                /// </summary>
                private async Task SprawdzCzyZostalaDopisanaDataWylogowania()
                {

                    var users = await _context.Users.ToListAsync();

                    // pobiera tylko i wyłącznie rekordy do których jest przypisana "01.01.0001 00:00:00" data wylogowania 
                    var logowania = await _context.Logowania
                        .Where(w => w.DataWylogowania == DateTime.Parse("01.01.0001 00:00:00"))
                        .OrderByDescending(o => o.DataLogowania)
                        .ToListAsync();


                    foreach (var user in users)
                    {
                        foreach (var logowanie in logowania)
                        {

                            var logowaniaUzytkownika = logowania.Where(w => w.UserId == user.Id).OrderByDescending(o => o.DataLogowania).ToList (); // pobranie wszytkich logowań dla danego użytkownika
                            List<Logowanie> logowaniaUzytkownikaKopia = logowaniaUzytkownika;
                            if (logowaniaUzytkownikaKopia.Count > 1)
                            {
                                logowaniaUzytkownikaKopia.RemoveAt(0); // usunięcie pierwszego rekordu i zajęcie się pozostałymi rekordami, ponieważ pierwszy rekord należy do zalogowanego użytkownika
                                foreach (var logowanieUzytkownikaKopia in logowaniaUzytkownikaKopia)
                                {
                                    var cp = DateTime.Now - logowanieUzytkownikaKopia.DataLogowania;
                                    TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
                                    logowanieUzytkownikaKopia.DataWylogowania = DateTime.Now;
                                    logowanieUzytkownikaKopia.CzasPracy = czasPracy;

                                    _context.Entry(logowanieUzytkownikaKopia).State = EntityState.Modified;
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    }

                }
        */




        /// <summary>
        /// Przy zalogowaniu sprawdza czy do poprzedniego wylogowania została dopisana data wylogowania, jeśli nie to dopisuje
        /// Operacja wykonywana dla pojedyńczego użytkownika
        /// </summary>
        private async Task SprawdzCzyZostalaDopisanaDataWylogowania(string email)
        {
            var zalogowanyUser = await _context.Users.FirstOrDefaultAsync (f=> f.Email == email);
            if (zalogowanyUser != null)
            {
                // poniższe operacje mogą być wykonywane przez użytkownika oraz przez administratora
                // administrator sprawdza wszystkich użytkowników, a pojedyńczy user tylko dla siebie

                switch (zalogowanyUser.RoleName)
                {
                    case "Administrator": 
                        // znalezienie wszystkich rekoród posiadających "01.01.0001 00:00:00" w DataWylogowania
                        var logowania = await _context.Logowania
                            .Include(i => i.User)
                            .Where(w => w.DataWylogowania == DateTime.Parse("01.01.0001 00:00:00"))
                            .ToListAsync();


                        Dictionary<string, List<Logowanie>> results = new Dictionary<string, List<Logowanie>>();
                        // najpierwsz pętla przechodzi przez wszystkich użytkowników aby pobrać "01.01.0001 00:00:00" dla indywidualnych użytkowników
                        foreach (var user in await _context.Users.ToListAsync ())
                        {
                            // pętla przez wszystkie powyższe logowania
                            foreach (var logowanie in logowania)
                            {
                                if (logowanie.UserId == user.Id)
                                {
                                    List<Logowanie> logowaniaUzytkownika = new List<Logowanie>();
                                    // jeżeli użytkownik jest już w bazie dodaj do niego kolejny rekord z jego zalogowaniem
                                    if (results.ContainsKey(user.Email))
                                    {
                                        logowaniaUzytkownika = results[user.Email];
                                        logowaniaUzytkownika.Add(logowanie);
                                    }
                                    else
                                    {
                                        logowaniaUzytkownika.Clear();
                                        logowaniaUzytkownika.Add(logowanie);
                                        results.Add(user.Email, logowaniaUzytkownika);
                                    }
                                }
                            }
                        }
                        // usunięcie rekordów logowania według elementeów znajdujących się w słowniku
                        foreach (var result in results)
                        {
                            result.Value.OrderByDescending(o => o.DataLogowania); // posortowanie elementów wg. daty logowania
                            int iloscElementow = result.Value.Count;
                            var lastElementLogowanie = result.Value.Last();


                            var now = DateTime.Now.AddDays(-2); // usinięcie rekordów sprzed dwóch dni

                            if (lastElementLogowanie.DataLogowania < now)
                            {
                                // usunięcie logowania z bazy
                                _context.Logowania.Remove(lastElementLogowanie);
                                await _context.SaveChangesAsync();
                            }
                        }

                        break;


                    case "User":
/*
                        // pobiera logowania zalogowanego użytkownika bez dopisanej daty wylogowania
                        var logowaniaUzytkownika = await _context.Logowania
                            .Where(w => w.UserId == zalogowanyUser.Id && w.DataWylogowania == DateTime.Parse("01.01.0001 00:00:00"))
                            .OrderByDescending(o => o.DataLogowania)
                            .ToListAsync();

                        if (logowaniaUzytkownika.Count > 1)
                        {
                            foreach (var logowanieUzytkownika in logowaniaUzytkownika)
                            {
                                logowanieUzytkownika.DodajDateWylogowania(
                                    dataWylogowania: DateTime.Now
                                    );

                                _context.Entry(logowanieUzytkownika).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                        }
*/
                        break;
                }

            }
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
