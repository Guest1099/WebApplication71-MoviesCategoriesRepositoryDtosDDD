using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var result = await _rolesService.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var roles = result.Object;

                if (roles == null)
                    return View("NotFound");


                return View(new GetRolesDto()
                {
                    Roles = roles,
                    Paginator = Paginator<GetRoleDto>.CreateAsync(roles, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, GetRolesDto model)
        {
            NI.Navigation = Navigation.RolesIndex;
            try
            {
                var result = await _rolesService.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var roles = result.Object;
                if (roles == null)
                    return View("NotFound");


                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                {
                    roles = roles.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();
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
                model.PageIndex = Math.Min(model.PageIndex, (int)Math.Ceiling((double)roles.Count / model.PageSize));
                model.Paginator = Paginator<GetRoleDto>.CreateAsync(roles, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.RolesCreate;
            return View ();
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
