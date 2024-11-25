﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Categories;
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
            try
            {
                var result = await _categoriesRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var categories = result.Object;

                if (categories == null)
                    return View("NotFound");


                return View(new GetCategoriesDto()
                {
                    Paginator = Paginator<GetCategoryDto>.CreateAsync(categories, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, GetCategoriesDto model)
        {
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

                model.Paginator = Paginator<GetCategoryDto>.CreateAsync(categories, model.PageIndex, model.PageSize);
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
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
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
                    return View ("NotFound");

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
