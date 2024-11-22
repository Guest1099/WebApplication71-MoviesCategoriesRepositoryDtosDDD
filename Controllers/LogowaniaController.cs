using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication71.DTOs.Logowania;
using WebApplication71.Models;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;
using WebApplication71.Models.Enums;
using System.Linq;

namespace WebApplication71.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class LogowaniaController : Controller
    {
        private readonly ILogowaniaRepository _logowaniaRepository;
        private readonly IUsersService _usersService;

        public LogowaniaController(ILogowaniaRepository logowaniaRepository, IUsersService usersService)
        {
            _logowaniaRepository = logowaniaRepository;
            _usersService = usersService;
        }



        [HttpGet]
        public async Task<IActionResult> Index(GetLogowaniaDto model)
        {
            NI.Navigation = Navigation.LogowaniaIndex;
            try
            {
                /*if (model.DataZalogowaniaOd.ToShortDateString() == "01.01.0001 00:00:00")
                    model.DataZalogowaniaOd = DateTime.Now.AddMonths(-30);

                if (model.DataZalogowaniaDo.ToShortDateString() == "01.01.0001 00:00:00")
                    model.DataZalogowaniaDo = DateTime.Now;*/

                //model.DataZalogowaniaOd = DateTime.Now.AddMonths(-30);
                //model.DataZalogowaniaDo = DateTime.Now;



                return await SearchAndFiltringResutl(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, GetLogowaniaDto model)
        {
            NI.Navigation = Navigation.RolesIndex;
            try
            {
                return await SearchAndFiltringResutl(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        private async Task<IActionResult> SearchAndFiltringResutl(GetLogowaniaDto model)
        {
            if (model == null)
                return View("NotFound");

            var result = await _logowaniaRepository.GetAll(User.Identity.Name);

            if (result == null || !result.Success)
                return View("NotFound");

            var logowania = result.Object;
            if (logowania == null)
                return View("NotFound");



            model.ShowPaginator = true;
            model.DisplayNumersListAndPaginatorLinks = true;
            model.DisplayButtonLeftTrzyKropki = false;
            model.DisplayButtonRightTrzyKropki = false;
            model.SortowanieOptionItems = new SelectList(new List<string>() { "Email A-Z", "Email Z-A", "Data zalogowania rosnąco", "Data zalogowania malejąco", "Użytkownik obecnie zalogowany" }, "Data zalogowania malejąco");

/*
            model.DataZalogowaniaOd = DateTime.Now.AddMonths(-30);
            model.DataZalogowaniaDo = DateTime.Now;
            */



            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
            {
                logowania = logowania.Where(
                    w =>
                        w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                        w.ImieInazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();

                if (logowania.Count < 5)
                {
                    model.DisplayNumersListAndPaginatorLinks = false;
                }
            }


            // filtrowanie według daty logowania od i do

            logowania = logowania.Where(
                w =>
                    w.DataLogowania >= model.DataZalogowaniaOd &&
                    w.DataLogowania <= model.DataZalogowaniaDo
                    ).ToList();



            // Sortowanie
            switch (model.SortowanieOption)
            {
                case "Email A-Z":
                    logowania = logowania.OrderBy(o => o.Email).ToList();
                    break;

                case "Email Z-A":
                    logowania = logowania.OrderByDescending(o => o.Email).ToList();
                    break;

                case "Data zalogowania rosnąco":
                    logowania = logowania.OrderBy(o => o.DataLogowania).ToList();
                    break;

                case "Data zalogowania malejąco":
                    logowania = logowania.OrderByDescending(o => o.DataLogowania).ToList();
                    break;

                case "Użytkownik obecnie zalogowany":
                    logowania = logowania.OrderBy(o => o.DataWylogowania).ToList();
                    break;
            }



            model.Logowania = logowania;
            model.Paginator = Paginator<GetLogowanieDto>.CreateAsync(logowania, model.PageIndex, model.PageSize);





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
            int iloscWszystkichElementow = logowania.Count + nextPage;

            // * jeśli chcesz uogólnić wyświetlanie filtrowanych wyników usuń poniższe warunki, a filtrowania nadal będzie działało poprawnie
            if (5 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5" });
            if (10 * model.PageIndex <= iloscWszystkichElementow)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", }, "10");
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
                        model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10", }, "10");
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
            if (logowania.Count <= 5 && model.PageIndex == model.Paginator.TotalPage)
                model.ShowPaginator = false;


            // linki paginacji nie są wyświetlane jeśli ilość elementów na stronie oraz w bazie jest mniejsza od 10
            /*if (roles.Count < 10 && model.PageIndex == 1 && model.Paginator.TotalPage == 1)
                model.DisplayNumersListAndPaginatorLinks = false;*/




            // dlugość paginacji zależna od ilości elementów znajdujących się na liście, im więcej elementów tym więcej elementów w paginacji

            int ilosc = 9;
            if (logowania.Count < 10)
                ilosc = 5;
            if (logowania.Count > 10 && logowania.Count < 50)
                ilosc = 7;
            if (logowania.Count > 50)
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
            NI.Navigation = Navigation.LogowaniaCreate;
            try
            {
                var result = await _usersService.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");


                var users = result.Object;
                if (users == null)
                    return View("NotFound");

                var usersSelectList = users.Select(
                        s => new GetUserListDto()
                        {
                            Id = s.Id,
                            ImieInazwisko = $"{s.Imie} {s.Nazwisko}"
                        });

                if (usersSelectList == null)
                    return View("NotFound");


                return View(new CreateLogowanieDto()
                {
                    UsersList = new SelectList(usersSelectList, "Id", "ImieInazwisko")
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLogowanieDto model)
        {
            NI.Navigation = Navigation.LogowaniaCreate;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _logowaniaRepository.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Logowania");


                    // zwraca komunikat błędu związanego z tworzeniem rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }

                var resultUsers = await _usersService.GetAll();

                if (resultUsers == null || !resultUsers.Success)
                    return View("NotFound");


                var users = resultUsers.Object;
                if (users == null)
                    return View("NotFound");

                var usersSelectList = users.Select(
                        s => new GetUserListDto()
                        {
                            Id = s.Id,
                            ImieInazwisko = $"{s.Imie} {s.Nazwisko}"
                        });

                if (usersSelectList == null)
                    return View("NotFound");


                model.UsersList = new SelectList(usersSelectList, "Id", "ImieInazwisko", model.UserId);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }





        [HttpGet]
        public async Task<IActionResult> Edit(string logowanieId)
        {
            NI.Navigation = Navigation.LogowaniaEdit;
            try
            {
                if (string.IsNullOrEmpty(logowanieId))
                    return NotFound();

                var result = await _logowaniaRepository.Get(logowanieId);

                if (result == null || !result.Success)
                    return NotFound();


                var logowanie = result.Object;
                if (logowanie == null)
                    return NotFound();


                return View(logowanie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetLogowanieDto model)
        {
            NI.Navigation = Navigation.LogowaniaEdit;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _logowaniaRepository.Update(new EditLogowanieDto()
                    {
                        LogowanieId = model.LogowanieId,
                        DataLogowania = model.DataLogowania,
                        DataWylogowania = model.DataWylogowania,
                        Email = model.Email
                    });
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Logowania");


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
        public IActionResult Delete(string id)
        {
            NI.Navigation = Navigation.LogowaniaDelete;
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

                var result = await _logowaniaRepository.Delete(id);
                if (result == null || !result.Success)
                    return View("NotFound");

                return RedirectToAction("Index", "Logowania");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}
