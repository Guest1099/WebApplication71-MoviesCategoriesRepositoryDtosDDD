using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Users;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
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
            NI.Navigation = Navigation.UsersIndex;
            try
            {
                return await SearchAndFiltringResutl(model);
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
            NI.Navigation = Navigation.UsersIndex;
            try
            {
                return await SearchAndFiltringResutl(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        private async Task<IActionResult> SearchAndFiltringResutl(GetUsersDto model)
        {
            if (model == null)
                return View("NotFound");

            var result = await _usersService.GetAll();

            if (result == null || !result.Success)
                return View("NotFound");

            var users = result.Object;
            if (users == null)
                return View("NotFound");


            model.ShowPaginator = true;
            model.DisplayNumersListAndPaginatorLinks = true;
            model.DisplayButtonLeftTrzyKropki = false;
            model.DisplayButtonRightTrzyKropki = false;
            model.SelectListSearchOptionItems = new SelectList(new List<string>() { "Email", "Nazwisko", "Wszędzie" }, "Wszędzie");
            model.SortowanieOptionItems = new SelectList(new List<string>() { "Email A-Z", "Email Z-A", "Nazwisko A-Z", "Nazwisko Z-A" }, "Nazwa A-Z");



            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
            {
                switch (model.SearchOption)
                {
                    case "Email":
                        users = users.Where(w => w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case "Nazwisko":
                        users = users.Where(w => w.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case "Wszędzie":
                        users = users.Where(
                            w =>
                            w.Imie.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                            w.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase) 
                            /*w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                            w.RoleName.Contains(model.q, StringComparison.OrdinalIgnoreCase)*/
                            ).ToList();
                        break;
                }

                if (users.Count < 5)
                {
                    model.DisplayNumersListAndPaginatorLinks = false;
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





            // ustawienie dla wszystkich stron paginacji możliwą ilość wyświetlenia elementów na stronie
            switch (model.PageSize)
            {
                case 5:
                    model.SelectListNumberItems = new SelectList(new List<string>() { "5" });
                    break;

                case 10:
                    model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", }, "10");
                    break;

                case 15:
                    model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15" }, "15");
                    break;

                case 20:
                    model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15", "20" }, "20");
                    break;
            }




            int nextPage = 4; // + kolejne 4, czyli wartość jaką należy dodać aby uzyskać wynik kolejnej PageSize
            int iloscWszystkichElementow = users.Count + nextPage;

            // * jeśli chcesz uogólnić wyświetlanie filtrowanych wyników usuń poniższe warunki, a filtrowania nadal będzie działało poprawnie
            if (5 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5" });
            if (10 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10" }, "10");
            if (15 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15" }, "15");
            if (20 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15", "20" }, "20");


            // alternatywny sposób do powyższego
            /*if (iloscObecnychElementow > iloscWszystkichElementow)
                model.Roles = new List<GetRoleDto>();*/




            // obliczenia dla ostatniej strony
            model.LastPage = model.PageIndex == model.Paginator.TotalPage;
            if (model.LastPage)
            {
                model.DisplayNumersListAndPaginatorLinks = true;
                switch (model.PageSize)
                {
                    case 5:
                        model.SelectListNumberItems = new SelectList(new List<string>() { "5" });
                        break;

                    case 10:
                        model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", });
                        break;

                    case 15:
                        model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15" }, "15");
                        break;

                    case 20:
                        model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", "15", "20" }, "20");
                        break;
                }
            }



            if (model.PageIndex == 1 && model.Paginator.TotalPage <= 2 && model.Paginator.Count <= 5)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", }, "10");



            if (!string.IsNullOrEmpty(model.q) && model.PageIndex == 1 && model.Paginator.TotalPage == 1 && model.Paginator.Count <= 5)
                model.ShowPaginator = false;


            // paginator wyświetlany jest tylko wtedy gdy ilość elementów tabeli wynosi minimum 5
            if (users.Count <= 5 && model.PageIndex == model.Paginator.TotalPage)
                model.ShowPaginator = false;


            // linki paginacji nie są wyświetlane jeśli ilość elementów na stronie oraz w bazie jest mniejsza od 10
            /*if (roles.Count < 10 && model.PageIndex == 1 && model.Paginator.TotalPage == 1)
                model.DisplayNumersListAndPaginatorLinks = false;*/




            // dlugość paginacji zależna od ilości elementów znajdujących się na liście, im więcej elementów tym więcej elementów w paginacji

            int ilosc = 9;
            if (users.Count < 10)
                ilosc = 5;
            if (users.Count > 10 && users.Count < 50)
                ilosc = 7;
            if (users.Count > 50)
                ilosc = 9;

            model.End = ilosc;
            int srodek = (int)Math.Round((double)(ilosc / 2)) + 1;
            if (model.PageIndex > srodek)
            {
                model.Start = model.PageIndex - (srodek - 1);
                model.End = model.PageIndex + ilosc - srodek;
            }



            // button z trzema kropaki na końcu wyświetlany jest tylko wtedy gdy ilość stron paginacji jest większa od 9
            if (model.Paginator.TotalPage >= 6 && model.PageIndex >= 6)
                model.DisplayButtonLeftTrzyKropki = true;

            if (model.Paginator.TotalPage > ilosc)
                model.DisplayButtonRightTrzyKropki = true;



            return View(model);
        }





        [HttpGet]
        public async Task<IActionResult> Create()
        {
            NI.Navigation = Navigation.UsersCreate;

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
            NI.Navigation = Navigation.UsersCreate;

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
            NI.Navigation = Navigation.UsersEdit;
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
            NI.Navigation = Navigation.UsersEdit;
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
                        RoleName = model.RoleName,
                        //PhotosUser = model.PhotosUser,
                        Files = model.Files
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
            NI.Navigation = Navigation.UsersChangeEmail;
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
            NI.Navigation = Navigation.UsersChangeEmail;
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
            NI.Navigation = Navigation.UsersChangePassword;
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
            NI.Navigation = Navigation.UsersChangePassword;
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
            NI.Navigation = Navigation.UsersDelete;
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




        /*
                [HttpGet]
                public async Task<IActionResult> DeletePhotoUser(string userId, string photoUserId)
                {
                    try
                    {
                        var result = await _usersService.DeletePhotoUser (photoUserId);
                        return RedirectToAction("Edit", "Users", new { userId = userId });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal server error: {ex.Message}");
                    }
                }*/


        [HttpGet]
        public async Task<IActionResult> DeletePhotoUser(string userId, string photoUserId)
        {
            NI.Navigation = Navigation.UsersDelete;
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(photoUserId))
                    return View("NotFound");

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ActionName("DeletePhotoUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhotoUserConfirmed(string userId, string photoUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(photoUserId))
                    return View("NotFound");

                var result = await _usersService.DeletePhotoUser(photoUserId);
                return RedirectToAction("Edit", "Users", new { userId = userId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    
    }
}
