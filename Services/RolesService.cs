﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Roles;
using WebApplication71.DTOs.Users;
using WebApplication71.Models;
using WebApplication71.Services.Abs;

namespace WebApplication71.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<ResultViewModel<List<GetRoleDto>>> GetAll()
        {
            var returnResult = new ResultViewModel<List<GetRoleDto>>() { Success = false, Message = "", Object = new List<GetRoleDto>() };

            try
            {
                var roles = await _context.Roles.ToListAsync();
                if (roles != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = roles.Select(
                        s => new GetRoleDto()
                        {
                            Name = s.Name
                        }).ToList();
                }
                else
                {
                    returnResult.Message = "Roles was null";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: ${ex.Message}";
            }
            return returnResult;
        }


        public async Task<ResultViewModel<GetRoleDto>> Get(string roleId)
        {
            var returnResult = new ResultViewModel<GetRoleDto>() { Success = false, Message = "", Object = new GetRoleDto() };

            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == roleId);
                if (role != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = new GetRoleDto()
                    {
                        Name = role.Name
                    };
                }
                else
                {
                    returnResult.Message = "Roles was null";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: ${ex.Message}";
            }
            return returnResult;
        }



        public async Task<ResultViewModel<CreateRoleDto>> Create(CreateRoleDto model)
        {
            var returnResult = new ResultViewModel<CreateRoleDto>() { Success = false, Message = "", Object = new CreateRoleDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza czy rola nie istnieje
                    if ((await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.Name)) == null)
                    {
                        ApplicationRole role = new ApplicationRole(
                            name: model.Name
                            );

                        _context.Roles.Add(role);
                        await _context.SaveChangesAsync();

                        returnResult.Success = true;
                        returnResult.Object = model;
                    }
                    else
                    {
                        returnResult.Message = "Podana nazwa roli już istnieje";
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




        public async Task<ResultViewModel<EditRoleDto>> Update(EditRoleDto model)
        {
            var returnResult = new ResultViewModel<EditRoleDto>() { Success = false, Message = "", Object = new EditRoleDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza czy rola nie istnieje, jeżeli nie istnieje, tworzy nową rolę
                    var role = await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.Name);
                    if (role == null)
                    {
                        role.Update(
                            name: model.Name
                            );

                        _context.Entry(role).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        returnResult.Success = true;
                        returnResult.Object = model;
                    }
                    else
                    {
                        returnResult.Message = "Role was null";
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




        public async Task<ResultViewModel<bool>> Delete(string roleId)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(roleId))
            {
                try
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == roleId);
                    if (role != null)
                    {
                        _context.Roles.Remove(role);
                        await _context.SaveChangesAsync();

                        returnResult.Success = true;
                        returnResult.Object = true;
                    }
                    else
                    {
                        returnResult.Message = "Role was null";
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


        public async Task<List<GetUserDto>> UsersInRole(string roleName)
        {
            var usersInRole = (await _userManager.GetUsersInRoleAsync(roleName))
                .Select(
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
                    })
                .ToList();

            return usersInRole;
        }
    }
}
