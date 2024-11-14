using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Roles;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
using WebApplication71.Services;
using WebApplication71.Services.Abs;

namespace WebApplication71.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(GetRolesDto model)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, GetRolesDto model)
        {
            NI.Navigation = Navigation.RolesIndex;
            try
            {
                //model.PageIndex = 1;
                return await SearchAndFiltringResutl(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        private async Task<IActionResult> SearchAndFiltringResutl(GetRolesDto model)
        {
            if (model == null)
                return View("NotFound");

            var result = await _rolesService.GetAll();

            if (result == null || !result.Success)
                return View("NotFound");

            var roles = result.Object;
            if (roles == null)
                return View("NotFound");


            model.ShowPaginator = true;
            model.DisplayNumersListAndPaginatorLinks = true;
            model.DisplayButtonLeftTrzyKropki = false;
            model.DisplayButtonRightTrzyKropki = false;
            model.SortowanieOptionItems = new SelectList(new List<string>() { "Nazwa A-Z", "Nazwa Z-A" }, "Nazwa A-Z");


            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
            {
                roles = roles.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();

                if (roles.Count < 5)
                {
                    model.DisplayNumersListAndPaginatorLinks = false;
                }
            }




            // Sortowanie
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    roles = roles.OrderBy(o => o.Name).ToList();
                    break;

                case "Nazwa Z-A":
                    roles = roles.OrderByDescending(o => o.Name).ToList();
                    break;
            }



            model.Roles = roles;
            model.Paginator = Paginator<GetRoleDto>.CreateAsync(roles, model.PageIndex, model.PageSize);


                          


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




            int iloscWszystkichElementow = roles.Count;

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



            if (model.PageIndex == 1 && model.Paginator.TotalPage <= 2 && model.Paginator.Count <= 10)
                model.SelectListNumberItems = new SelectList(new List<string>() { "5", "10" }, "10");



            if (!string.IsNullOrEmpty (model.q) && model.PageIndex == 1 && model.Paginator.Count <= 15)
                model.ShowPaginator = false;


            // paginator wyświetlany jest tylko wtedy gdy ilość elementów tabeli wynosi minimum 5
            if (model.Paginator.Count < 5 && model.Paginator.PageIndex != model.Paginator.TotalPage)
                model.ShowPaginator = false;


            // linki paginacji nie są wyświetlane jeśli ilość elementów na stronie oraz w bazie jest mniejsza od 10
            /*if (roles.Count < 10 && model.PageIndex == 1 && model.Paginator.TotalPage == 1)
                model.DisplayNumersListAndPaginatorLinks = false;*/



             
            // dlugość paginacji zależna od ilości elementów znajdujących się na liście, im więcej elementów tym więcej elementów w paginacji

            int ilosc = 9;
            if (roles.Count < 10)
                ilosc = 5;
            if (roles.Count > 10 && roles.Count < 50)
                ilosc = 7;
            if (roles.Count > 50)
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
        public IActionResult Create()
        {
            NI.Navigation = Navigation.RolesCreate;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleDto model)
        {
            NI.Navigation = Navigation.RolesCreate;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _rolesService.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Roles");


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
        public async Task<IActionResult> Edit(string roleId)
        {
            NI.Navigation = Navigation.RolesEdit;
            try
            {
                if (string.IsNullOrEmpty(roleId))
                    return View("NotFound");

                var result = await _rolesService.Get(roleId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var role = result.Object;
                if (role == null)
                    return View("NotFound");


                return View(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetRoleDto model)
        {
            NI.Navigation = Navigation.RolesEdit;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _rolesService.Update(new EditRoleDto()
                    {
                        Id = model.Id,
                        Name = model.Name
                    });
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Roles");


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
            NI.Navigation = Navigation.RolesDelete;
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

                var result = await _rolesService.Delete(id);
                if (result == null || !result.Success)
                    return View("NotFound");

                return RedirectToAction("Index", "Roles");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
