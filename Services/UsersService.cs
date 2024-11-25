﻿using Application.Services.Abs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                var users = await _context.Users.ToListAsync();
                if (users != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = users.Select(
                        s => new GetUserDto()
                        {
                            Email = s.Email,
                            Imie = s.Imie,
                            Nazwisko = s.Nazwisko,
                            Ulica = s.Ulica,
                            Miejscowosc = s.Miejscowosc,
                            Wojewodztwo = s.Wojewodztwo,
                            Pesel = s.Pesel,
                            DataUrodzenia = s.DataUrodzenia,
                            Plec = s.Plec,
                            Telefon = s.Telefon,
                            Photo = s.Photo
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
                            Email = user.Email,
                            Imie = user.Imie,
                            Nazwisko = user.Nazwisko,
                            Ulica = user.Ulica,
                            Miejscowosc = user.Miejscowosc,
                            Wojewodztwo = user.Wojewodztwo,
                            Pesel = user.Pesel,
                            DataUrodzenia = user.DataUrodzenia,
                            Plec = user.Plec,
                            Telefon = user.Telefon,
                            Photo = user.Photo
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
                            Email = user.Email,
                            Imie = user.Imie,
                            Nazwisko = user.Nazwisko,
                            Ulica = user.Ulica,
                            Miejscowosc = user.Miejscowosc,
                            Wojewodztwo = user.Wojewodztwo,
                            Pesel = user.Pesel,
                            DataUrodzenia = user.DataUrodzenia,
                            Plec = user.Plec,
                            Telefon = user.Telefon,
                            Photo = user.Photo
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
                            pesel: model.Pesel,
                            dataUrodzenia: model.DataUrodzenia,
                            plec: model.Plec,
                            telefon: model.Telefon,
                            photo: model.Photo
                            );


                        var createResult = await _userManager.CreateAsync(user, model.Password);
                        if (createResult.Succeeded)
                        {
                            /*
                                                        // dodanie zdjęcia
                                                        await CreateNewPhoto(model.Files, user.Id);
                            */

                            // dodanie nowozarejestrowanego użytkownika do ról 


                            await _userManager.AddToRoleAsync(user, "User");
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));



                            returnResult.Message = "Zarejestrowano, sprawdź pocztę aby dokończyć rejestrację";
                            returnResult.Success = true;
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
                    var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                    if (user != null)
                    {
                        user.Update(
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            ulica: model.Ulica,
                            miejscowosc: model.Miejscowosc,
                            wojewodztwo: model.Wojewodztwo,
                            pesel: model.Pesel,
                            dataUrodzenia: model.DataUrodzenia,
                            plec: model.Plec,
                            telefon: model.Telefon,
                            photo: model.Photo
                            );


                        // zaktualizowanie danych użytkownika
                        await _userManager.UpdateAsync(user);

                        /*
                                                // dodanie zdjęcia
                                                await CreateNewPhoto(model.Files, user.Id);
                        */

                        /*
                                                // usunięcie użytkownika z poprzedniej roli
                                                await _userManager.RemoveFromRoleAsync (user, "User");

                                                // i przypisanie do kolejnej
                                                await _userManager.AddToRoleAsync(user, "User");
                                                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                        */


                        returnResult.Message = "Dane zostały zaktualizowane poprawnie";
                        returnResult.Success = true;
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




        public async Task<ResultViewModel<bool>> Delete(string userId)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
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

    }





}

