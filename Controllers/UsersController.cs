using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Categories;
using WebApplication71.DTOs.Users;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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

                model.Paginator = Paginator<GetUserDto>.CreateAsync(users, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet]
        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _usersService.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Users");


                    // zwraca komunikat błędu związanego z tworzeniem rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }

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
                    });

                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Users");


                    // zwraca komunikat błędu związanego z aktualizacją rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
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
