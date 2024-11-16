﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Logowania;
using WebApplication71.Models;
using WebApplication71.Repos.Abs;

namespace WebApplication71.Repos
{
    public class LogowaniaRepository : ILogowaniaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogowaniaRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<ResultViewModel<List<GetLogowanieDto>>> GetAll(string email)
        {
            var resultViewModel = new ResultViewModel<List<GetLogowanieDto>>() { Success = false, Message = "", Object = new List<GetLogowanieDto>() };
            try
            {
                var logowania = await _context.Logowania
                    .Include(i => i.User)
                    .OrderByDescending(o => o.DataLogowania)
                    .ToListAsync();


                var zalogowanyUser = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                if (zalogowanyUser != null)
                {
                    if (await _userManager.IsInRoleAsync(zalogowanyUser, "User"))
                    {
                        logowania = logowania.Where(w => w.User.Email == email).ToList();
                    }
                }


                if (logowania != null)
                {
                    resultViewModel.Success = true;
                    resultViewModel.Object = logowania.Select(
                        s => new GetLogowanieDto()
                        {
                            LogowanieId = s.LogowanieId,
                            DataLogowania = s.DataLogowania,
                            DataWylogowania = s.DataWylogowania,
                            CzasPracy = s.CzasPracy,
                            ImieInazwisko = $"{s.User.Imie} {s.User.Nazwisko}",
                            Email = s.User.Email
                        })
                            .ToList();
                }
                else
                {
                    resultViewModel.Message = "Logowania was null";
                }
            }
            catch (Exception ex)
            {
                resultViewModel.Message = $"Exception: {ex.Message}";
            }

            return resultViewModel;
        }




        public async Task<ResultViewModel<GetLogowanieDto>> Get(string logowanieId)
        {
            var resultViewModel = new ResultViewModel<GetLogowanieDto>() { Success = false, Message = "", Object = new GetLogowanieDto() };

            if (!string.IsNullOrEmpty(logowanieId))
            {
                try
                {
                    var logowanie = await _context.Logowania
                            .Include(i => i.User)
                            .FirstOrDefaultAsync(f => f.LogowanieId == logowanieId);
                    if (logowanie != null)
                    {
                        resultViewModel.Success = true;
                        resultViewModel.Object = new GetLogowanieDto()
                        {
                            LogowanieId = logowanie.LogowanieId,
                            DataLogowania = logowanie.DataLogowania,
                            DataWylogowania = logowanie.DataWylogowania,
                            CzasPracy = logowanie.CzasPracy,
                            ImieInazwisko = $"{logowanie.User.Imie} {logowanie.User.Nazwisko}",
                            Email = logowanie.User.Email
                        };
                    }
                    else
                    {
                        resultViewModel.Message = "Category was null";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Id was null";
            }

            return resultViewModel;
        }




        public async Task<ResultViewModel<CreateLogowanieDto>> Create(CreateLogowanieDto model)
        {
            var resultViewModel = new ResultViewModel<CreateLogowanieDto>() { Success = false, Message = "", Object = new CreateLogowanieDto() };

            if (model != null)
            {
                try
                {
                    if (model.DataLogowania < model.DataWylogowania)
                    {
                        Logowanie logowanie = new Logowanie(
                            dataLogowania: model.DataLogowania,
                            dataWylogowania: model.DataWylogowania,
                            userId: model.UserId
                            );

                        _context.Logowania.Add(logowanie);
                        await _context.SaveChangesAsync();

                        resultViewModel.Success = true;
                        resultViewModel.Object = model;
                    }
                    else
                    {
                        resultViewModel.Message = $"Data logowania musi być mniejsza od daty wylogowania";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Model was null";
            }
            return resultViewModel;
        }





        public async Task<ResultViewModel<EditLogowanieDto>> Update(EditLogowanieDto model)
        {
            var resultViewModel = new ResultViewModel<EditLogowanieDto>() { Success = false, Message = "", Object = new EditLogowanieDto() };

            if (model != null)
            {
                try
                {
                    var logowanie = await _context.Logowania.FirstOrDefaultAsync(f => f.LogowanieId == model.LogowanieId);
                    if (logowanie != null)
                    {
                        logowanie.Update(
                            dataLogowania: model.DataLogowania,
                            dataWylogowania: model.DataWylogowania,
                            userId: logowanie.UserId
                            );

                        _context.Entry(logowanie).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        resultViewModel.Success = true;
                        resultViewModel.Object = model;
                    }
                    else
                    {
                        resultViewModel.Message = "Logowanie was null";
                    }

                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Model was null";
            }
            return resultViewModel;
        }




        public async Task<ResultViewModel<bool>> Delete(string logowanieId)
        {
            var resultViewModel = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(logowanieId))
            {
                try
                {
                    var logowanie = await _context.Logowania.FirstOrDefaultAsync(f => f.LogowanieId == logowanieId);
                    if (logowanie != null)
                    {
                        _context.Logowania.Remove(logowanie);
                        await _context.SaveChangesAsync();

                        resultViewModel.Success = true;
                        resultViewModel.Object = true;
                    }
                    else
                    {
                        resultViewModel.Message = "Logowanie was null";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Id was null";
            }
            return resultViewModel;
        }

    }
}
