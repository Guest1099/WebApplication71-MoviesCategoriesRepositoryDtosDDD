using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Logowania;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize]
    public class LogowaniaController : Controller
    {
        private readonly ILogowaniaRepository _logowaniaRepository;

        public LogowaniaController(ILogowaniaRepository logowaniaRepository)
        {
            _logowaniaRepository = logowaniaRepository;
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


                return View(new GetLogowaniaDto()
                {
                    Paginator = Paginator<GetLogowanieDto>.CreateAsync(logowania, model.PageIndex, model.PageSize),
                    PageIndex = model.PageIndex,
                    PageSize = model.PageSize,
                    Start = model.Start,
                    End = model.End,
                    q = model.q,
                    SearchOption = model.SearchOption,
                    SortowanieOption = model.SortowanieOption,
                    DataZalogowaniaOd = DateTime.Now.ToString(),
                    DataZalogowaniaDo = DateTime.Now.ToString()
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
                        DateTime.Parse(w.DataLogowania).Year >= DateTime.Parse(model.DataZalogowaniaOd).Year &&
                        DateTime.Parse(w.DataLogowania).Month >= DateTime.Parse(model.DataZalogowaniaOd).Month &&
                        DateTime.Parse(w.DataLogowania).Day >= DateTime.Parse(model.DataZalogowaniaOd).Day
                    ).ToList();

                // Data logowania do
                logowania = logowania.Where(
                    w =>
                        DateTime.Parse(w.DataLogowania).Year <= DateTime.Parse(model.DataZalogowaniaDo).Year &&
                        DateTime.Parse(w.DataLogowania).Month <= DateTime.Parse(model.DataZalogowaniaDo).Month &&
                        DateTime.Parse(w.DataLogowania).Day <= DateTime.Parse(model.DataZalogowaniaDo).Day
                    ).ToList();




                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Data logowania rosnąco":
                        logowania = logowania.OrderBy(o => o.DataLogowania).ToList();
                        break;

                    case "Data logowania malejąco":
                        logowania = logowania.OrderByDescending(o => o.DataLogowania).ToList();
                        break;
                }

                model.Paginator = Paginator<GetLogowanieDto>.CreateAsync(logowania, model.PageIndex, model.PageSize);


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


                var category = result.Object;
                if (category == null)
                    return NotFound();


                return View(category);
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
