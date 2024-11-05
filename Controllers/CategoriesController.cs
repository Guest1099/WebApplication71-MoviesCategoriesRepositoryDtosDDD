using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Categories;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(GetCategoriesDto model)
        {
            NI.Navigation = Navigation.CategoriesIndex;
            try
            {
                var result = await _categoriesRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var categories = result.Object;

                if (categories == null)
                    return View("NotFound");


                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                {
                    categories = categories.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Nazwa A-Z":
                        categories = categories.OrderBy(o => o.Name).ToList();
                        break;

                    case "Nazwa Z-A":
                        categories = categories.OrderByDescending(o => o.Name).ToList();
                        break;
                }

                

                model.End = model.PageSize + 1;
                int srodek = (int)Math.Round((double)(model.PageSize / 2));
                if (model.PageIndex > srodek)
                {
                    model.Start = model.PageIndex - (srodek - 1);
                    model.End = model.PageIndex + model.PageSize - srodek;
                }


                model.Categories = categories;
                model.Paginator = Paginator<GetCategoryDto>.CreateAsync(categories, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, GetCategoriesDto model)
        {
            NI.Navigation = Navigation.CategoriesIndex;
            try
            {
                var result = await _categoriesRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var categories = result.Object;
                if (categories == null)
                    return View("NotFound");


                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                {
                    categories = categories.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Nazwa A-Z":
                        categories = categories.OrderBy(o => o.Name).ToList();
                        break;

                    case "Nazwa Z-A":
                        categories = categories.OrderByDescending(o => o.Name).ToList();
                        break;
                }

                model.End = model.PageSize + 1;
                int srodek = (int)Math.Round((double)(model.PageSize / 2));
                if (model.PageIndex > srodek)
                {
                    model.Start = model.PageIndex - (srodek - 1);
                    model.End = model.PageIndex + model.PageSize - srodek;
                }

                model.Categories = categories;
                model.PageIndex = Math.Min(model.PageIndex, (int)Math.Ceiling((double)categories.Count / model.PageSize));
                model.Paginator = Paginator<GetCategoryDto>.CreateAsync(categories, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task InitializeSearchAndFilterData ()
        {

        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.CategoriesCreate;
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            NI.Navigation = Navigation.CategoriesCreate;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoriesRepository.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Categories");


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
        public async Task<IActionResult> Edit(string categoryId)
        {
            NI.Navigation = Navigation.CategoriesEdit;
            try
            {
                if (string.IsNullOrEmpty(categoryId))
                    return View("NotFound");

                var result = await _categoriesRepository.Get(categoryId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var category = result.Object;
                if (category == null)
                    return View("NotFound");


                return View(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetCategoryDto model)
        {
            NI.Navigation = Navigation.CategoriesEdit;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoriesRepository.Update(new EditCategoryDto()
                    {
                        CategoryId = model.CategoryId,
                        Name = model.Name
                    });
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Categories");


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
            NI.Navigation = Navigation.CategoriesDelete;
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

                var result = await _categoriesRepository.Delete(id);
                if (result == null || !result.Success)
                    return View("NotFound");

                return RedirectToAction("Index", "Categories");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
