using Application.Services.Abs;
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
using WebApplication71.DTOs.Users;
using WebApplication71.Models;

namespace WebApplication71.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResultViewModel<List<GetUserDto>>> GetAll()
        {
            var returnResult = new ResultViewModel<List<GetUserDto>>() { Success = false, Message = "", Object = new List<GetUserDto>() };

            try
            {
                var users = await _context.Users
                    .OrderByDescending(o => o.DataDodania)
                    .ToListAsync();

                if (users != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = users.Select(
                        s => new GetUserDto()
                        {
                            Id = s.Id,
                            Email = s.Email,
                            Imie = s.Imie,
                            Nazwisko = s.Nazwisko,
                            Ulica = s.Ulica,
                            Miejscowosc = s.Miejscowosc,
                            Wojewodztwo = s.Wojewodztwo,
                            KodPocztowy = s.KodPocztowy,
                            Pesel = s.Pesel,
                            DataUrodzenia = s.DataUrodzenia,
                            Plec = s.Plec,
                            Telefon = s.Telefon,
                            Photo = s.Photo,
                            RoleName = s.RoleName,
                            DataDodania = s.DataDodania
                        }).ToList();
                }
                else
                {
                    returnResult.Message = "Users was null";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: ${ex.Message}";
            }
            return returnResult;
        }



        public async Task<ResultViewModel<GetUserDto>> GetUserById(string userId)
        {
            var returnResult = new ResultViewModel<GetUserDto>() { Success = false, Message = "", Object = new GetUserDto() };

            if (!string.IsNullOrEmpty(userId))
            {
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
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: ${ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Id was null";
            }
            return returnResult;
        }



        public async Task<ResultViewModel<GetUserDto>> GetUserByEmail(string email)
        {
            var returnResult = new ResultViewModel<GetUserDto>() { Success = false, Message = "", Object = new GetUserDto() };

            if (!string.IsNullOrEmpty(email))
            {
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
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: ${ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Email was null";
            }
            return returnResult;
        }





        public async Task<ResultViewModel<CreateUserDto>> Create(CreateUserDto model)
        {
            var returnResult = new ResultViewModel<CreateUserDto>() { Success = false, Message = "", Object = new CreateUserDto() };

            if (model != null)
            {
                try
                {
                    // warunek sprawdza czy konto istnieje
                    if (await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email) == null)
                    {
                        // jeżeli nie tworzy nowego użytkownika

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
                            photo: await ChangeFileToBytes(model.PhotoData),
                            roleName: model.RoleName
                            );


                        var createResult = await _userManager.CreateAsync(user, model.Password);
                        if (createResult.Succeeded)
                        {

                            user.EmailConfirmed = false;
                            user.LockoutEnabled = false;
                            user.SecurityStamp = Guid.NewGuid().ToString();
                            await _userManager.UpdateAsync(user);


                            /*
                                                        // dodanie zdjęcia
                                                        await CreateNewPhoto(model.Files, user.Id);
                            */

                            // dodanie nowozarejestrowanego użytkownika do ról 

                            await _userManager.AddToRoleAsync(user, model.RoleName);
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.RoleName));


                            returnResult.Success = true;
                            returnResult.Message = "Zarejestrowano, sprawdź pocztę aby dokończyć rejestrację";
                            returnResult.Object = model;
                        }
                        else
                        {
                            returnResult.Message = "Użytkownik nie został zarejestrowany";
                        }
                    }
                    else
                    {
                        returnResult.Message = "Nazwa emaila jest już zajęta";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: ${ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }
            return returnResult;
        }






        public async Task<ResultViewModel<EditUserDto>> Update(EditUserDto model)
        {
            var returnResult = new ResultViewModel<EditUserDto>() { Success = false, Message = "", Object = new EditUserDto() };

            if (model != null)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == model.Id);
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
                            photo: photo,
                            roleName: model.RoleName
                            );


                        // zaktualizowanie danych użytkownika

                        user.EmailConfirmed = false;
                        user.LockoutEnabled = false;
                        user.SecurityStamp = Guid.NewGuid ().ToString ();
                        await _userManager.UpdateAsync(user);


                        // dodanie zdjęcia
                        //await CreateNewPhoto(model.Files, user.Id);



                        // usunięcie użytkownika z poprzednich ról
                        var roles = _context.Roles.ToList().Select(s => s.Name).ToList();
                        await _userManager.RemoveFromRolesAsync(user, roles);

                        // i przypisanie do kolejnej
                        await _userManager.AddToRoleAsync(user, model.RoleName);
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.RoleName));



                        returnResult.Success = true;
                        returnResult.Message = "Dane zostały zaktualizowane poprawnie";
                        returnResult.Object = model;

                    }
                    else
                    {
                        returnResult.Message = "Dane nie zostały zaktualizowane";
                    }
                }


                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: ${ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }
            return returnResult;
        }








        public async Task<ResultViewModel<ChangeUserEmailDto>> ChangeEmail(ChangeUserEmailDto model)
        {
            var returnResult = new ResultViewModel<ChangeUserEmailDto>() { Success = false, Message = "", Object = new ChangeUserEmailDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza na podstawie nowego maila czy użytkownik już istnieje, jeśli tak zwraca informację
                    // jeśli nie aktualizuje maila
                    if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.NewEmail)) == null)
                    {
                        ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
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
                                user.SecurityStamp = Guid.NewGuid ().ToString ();
                                await _userManager.UpdateAsync(user);

                                //await _signInManager.SignOutAsync ();

                                // dołącz to pole jeżeli w polu Email w vidoku ma się zaktualizować email
                                //model.Email = user.Email; 

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




        /// <summary>
        /// Tutaj aby zmienić hasło najpierw trzeba usunąć użytkownika, stworzyć go na nowo i przypisać do niego nowe hasło
        /// </summary>
        public async Task<ResultViewModel<ChangeUserPasswordDto>> ChangePassword(ChangeUserPasswordDto model)
        {
            var returnResult = new ResultViewModel<ChangeUserPasswordDto>() { Success = false, Message = "", Object = new ChangeUserPasswordDto() };

            if (model != null)
            {
                try
                {
                    ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Id == model.Id);
                    if (user != null)
                    {
                        bool deleteResult = false;
                        bool createResult = false;


                        // usunięcie użytkownika
                        if ((await Delete(user.Id)).Success)
                            deleteResult = true;


                        // utworzenie nowego użytkownika
                        if (await ChangePasswordCreateUser(user, model.NewPassword))
                            createResult = true;



                        bool deleteAndCreateResult = deleteResult == true;
                        returnResult.Success = deleteAndCreateResult;


                        if (returnResult.Success)
                        {
                            returnResult.Message = "Hasło zostało zmienione poprawnie";
                        }
                        else
                        {
                            returnResult.Message = "Hasło nie zostało zmienione";
                        }

                        returnResult.Object = model;
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
        /// Metoda wykorzystywana tylko i wyłączie w metodzie ChangePassword do stworzenia nowego użytkownika
        /// </summary>
        private async Task<bool> ChangePasswordCreateUser(ApplicationUser user, string newPassword)
        {
            var result = false;

            // jeżeli nie tworzy nowego użytkownika

            var u = new ApplicationUser(
                email: user.Email,
                imie: user.Imie,
                nazwisko: user.Nazwisko,
                ulica: user.Ulica,
                miejscowosc: user.Miejscowosc,
                wojewodztwo: user.Wojewodztwo,
                kodPocztowy: user.KodPocztowy,
                pesel: user.Pesel,
                dataUrodzenia: user.DataUrodzenia,
                plec: user.Plec,
                telefon: user.Telefon,
                photo: user.Photo,
                roleName: user.RoleName,
                password: newPassword,
                dataDodania: user.DataDodania
                );


            var createResult = await _userManager.CreateAsync(user, newPassword);
            if (createResult.Succeeded)
            {

                user.EmailConfirmed = false;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                await _userManager.UpdateAsync(user);


                /*
                                            // dodanie zdjęcia
                                            await CreateNewPhoto(model.Files, user.Id);
                */

                // dodanie nowozarejestrowanego użytkownika do ról 

                await _userManager.AddToRoleAsync(user, u.RoleName);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, u.RoleName));


                result = true;
            }
            return result;
        }





        public async Task<ResultViewModel<bool>> Delete(string userId)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var user = await _userManager.Users.FirstOrDefaultAsync(f => f.Id == userId);
                    if (user != null)
                    {

                        // usunięcie filmów
                        var movies = await _context.Movies.Where(w => w.UserId == userId).ToListAsync();
                        foreach (var movie in movies)
                            _context.Movies.Remove(movie);

                        // usunięcie logowań
                        var logowania = await _context.Logowania.Where(w => w.UserId == userId).ToListAsync();
                        foreach (var logowanie in logowania)
                            _context.Logowania.Remove(logowanie);


                        var deleteResult = await _userManager.DeleteAsync(user);
                        if (deleteResult.Succeeded)
                        {
                            returnResult.Success = true;
                            returnResult.Object = true;
                        }
                    }
                    else
                    {
                        returnResult.Message = "User was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: ${ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Id was null";
            }
            return returnResult;
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

