using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Logowania;
using WebApplication71.Models;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize]
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
            try
            {
                var result = await _logowaniaRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var logowania = result.Object;

                if (logowania == null)
                    return View("NotFound");


                if (model.DataZalogowaniaOd.ToShortDateString() == "01.01.0001")
                    model.DataZalogowaniaOd = DateTime.Now.AddMonths(-30);

                if (model.DataZalogowaniaDo.ToShortDateString() == "01.01.0001")
                    model.DataZalogowaniaDo = DateTime.Now;


                return View(new GetLogowaniaDto()
                {
                    Logowania = logowania,
                    Paginator = Paginator<GetLogowanieDto>.CreateAsync(logowania, model.PageIndex, model.PageSize),
                    PageIndex = model.PageIndex,
                    PageSize = model.PageSize,
                    Start = model.Start,
                    End = model.End,
                    q = model.q,
                    SearchOption = model.SearchOption,
                    SortowanieOption = model.SortowanieOption,
                    DataZalogowaniaOd = model.DataZalogowaniaOd,
                    DataZalogowaniaDo = model.DataZalogowaniaDo,
                });
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
            try
            {
                var result = await _logowaniaRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var logowania = result.Object;

                if (logowania == null)
                    return View("NotFound");


                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                {
                    logowania = logowania.Where(
                        w =>
                            w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                        ).ToList();
                }


                // Data logowania od
                logowania = logowania.Where(
                    w =>
                        w.DataLogowania.Year >= model.DataZalogowaniaOd.Year &&
                        w.DataLogowania.Month >= model.DataZalogowaniaOd.Month &&
                        w.DataLogowania.Day >= model.DataZalogowaniaOd.Day
                    ).ToList();

                // Data logowania do
                logowania = logowania.Where(
                    w =>
                        w.DataLogowania.Year <= model.DataZalogowaniaDo.Year &&
                        w.DataLogowania.Month <= model.DataZalogowaniaDo.Month &&
                        w.DataLogowania.Day <= model.DataZalogowaniaDo.Day
                    ).ToList();




                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Data zalogowania rosnąco":
                        logowania = logowania.OrderBy(o => o.DataLogowania).ToList();
                        break;

                    case "Data zalogowania malejąco":
                        logowania = logowania.OrderByDescending(o => o.DataLogowania).ToList();
                        break;
                }


                model.Logowania = logowania;
                model.PageIndex = Math.Min(model.PageIndex, (int)Math.Ceiling((double)logowania.Count / model.PageSize));
                model.Paginator = Paginator<GetLogowanieDto>.CreateAsync(logowania, model.PageIndex, model.PageSize);
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
