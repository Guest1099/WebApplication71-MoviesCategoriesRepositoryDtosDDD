using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Users;
using WebApplication71.Services;
using WebApplication71.Services.Abs;

namespace WebApplication71.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public UsersController(IUsersService usersService, IRolesService rolesService)
        {
            _usersService = usersService;
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(GetUsersDto model)
        {
            try
            {
                var result = await _usersService.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var users = result.Object;

                if (users == null)
                    return View("NotFound");


                return View(new GetUsersDto()
                {
                    Users = users,
                    Paginator = Paginator<GetUserDto>.CreateAsync(users, model.PageIndex, model.PageSize),
                    PageIndex = model.PageIndex,
                    PageSize = model.PageSize,
                    Start = model.Start,
                    End = model.End,
                    q = model.q,
                    SearchOption = model.SearchOption,
                    SortowanieOption = model.SortowanieOption
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, GetUsersDto model)
        {
            try
            {
                var result = await _usersService.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var users = result.Object;
                if (users == null)
                    return View("NotFound");


                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                {
                    users = users.Where(
                        w =>
                            w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                            w.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                        ).ToList();
                }


                // Opcje wyszukiwania 
                if (!string.IsNullOrEmpty(model.q) && !string.IsNullOrEmpty (model.SearchOption))
                {
                    switch (model.SearchOption)
                    {
                        case "Email":
                            users = users.Where(w => w.Email.Contains(model.q)).ToList();
                            break;

                        case "Nazwisko":
                            users = users.Where(w => w.Nazwisko.Contains(model.q)).ToList();
                            break;

                        case "Wszędzie": break;
                    }
                }

                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Email A-Z":
                        users = users.OrderBy(o => o.Email).ToList();
                        break;

                    case "Email Z-A":
                        users = users.OrderByDescending(o => o.Email).ToList();
                        break;

                    case "Nazwisko A-Z":
                        users = users.OrderBy(o => o.Nazwisko).ToList();
                        break;

                    case "Nazwisko Z-A":
                        users = users.OrderByDescending(o => o.Nazwisko).ToList();
                        break;
                }

                model.Users = users;
                model.Paginator = Paginator<GetUserDto>.CreateAsync(users, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // zwraca komunikat błędu związanego z tworzeniem rekordu
            var rolesNames = (await _rolesService.GetAll()).Object
                            .Select(s => s.Name)
                            .ToList();

            return View(new CreateUserDto()
            {
                RolesList = new SelectList(rolesNames, "User")
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            try
            {
                /*if (ModelState.IsValid)
                {*/
                    var result = await _usersService.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Users");


                    // zwraca komunikat błędu związanego z tworzeniem rekordu
                    ViewData["ErrorMessage"] = result.Message;
                /*}*/



                // pobiera tylko i wyłącznie nazwy ról bez całego obiektu
                var rolesNames = (await _rolesService.GetAll()).Object
                            .Select(s => s.Name)
                            .ToList();

                model.RolesList = new SelectList(rolesNames, model.RoleName);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }





        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return View("NotFound");

                var result = await _usersService.GetUserById(userId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var user = result.Object;
                if (user == null)
                    return View("NotFound");



                // pobiera tylko i wyłącznie nazwy ról bez całego obiektu
                var rolesNames = (await _rolesService.GetAll()).Object
                            .Select(s => s.Name)
                            .ToList();

                user.RolesList = new SelectList(rolesNames);
                return View(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetUserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _usersService.Update(new EditUserDto()
                    {
                        Id = model.Id,
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        Ulica = model.Ulica,
                        Miejscowosc = model.Miejscowosc,
                        Wojewodztwo = model.Wojewodztwo,
                        KodPocztowy = model.KodPocztowy,
                        Pesel = model.Pesel,
                        DataUrodzenia = model.DataUrodzenia,
                        Plec = model.Plec,
                        Telefon = model.Telefon,
                        Photo = model.Photo,
                        PhotoData = model.PhotoData,
                        RoleName = model.RoleName
                    });

                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Users");


                    // zwraca komunikat błędu związanego z aktualizacją rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }


                // pobiera tylko i wyłącznie nazwy ról bez całego obiektu
                var rolesNames = (await _rolesService.GetAll()).Object
                            .Select(s => s.Name)
                            .ToList();

                model.RolesList = new SelectList(rolesNames, model.RoleName);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }





        [HttpGet]
        public async Task<IActionResult> ChangeEmail(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return View("NotFound");

                var result = await _usersService.GetUserById(userId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var user = result.Object;
                if (user == null)
                    return View("NotFound");


                return View(new ChangeUserEmailDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeUserEmailDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _usersService.ChangeEmail(model);
                    if (result != null && result.Success)
                    {
                        // ustawienie tego pola spowoduje, że w polu Email pojawia się nowy email, można to ustawić opcjonalnie
                        //model.Email = model.NewEmail;
                    }
                    // zwraca komunikat związany z aktualizacją rekordu
                    model.Message = result.Message;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet]
        public async Task<IActionResult> ChangePassword(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return View("NotFound");

                var result = await _usersService.GetUserById(userId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var user = result.Object;
                if (user == null)
                    return View("NotFound");


                return View(new ChangeUserPasswordDto()
                {
                    Id = user.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // sprawdza czy pola nowe hasło oraz confirm password są takie same, jeśli nie zwracany jest komunikat
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        model.Message = "Hasło muszą być identyczne";
                    }
                    else
                    {
                        var result = await _usersService.ChangePassword(model);

                        // zwraca komunikat związany z aktualizacją rekordu
                        model.Message = result.Message;
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return View("NotFound");
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return View("NotFound");

                var result = await _usersService.Delete(id);
                if (result == null || !result.Success)
                    return View("NotFound");

                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
