using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Movies;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public MoviesController(IMoviesRepository moviesRepository, ICategoriesRepository categoriesRepository)
        {
            _moviesRepository = moviesRepository;
            _categoriesRepository = categoriesRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(GetMoviesDto model)
        {
            NI.Navigation = Navigation.MoviesIndex;


            try
            {
                var result = await _moviesRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var movies = result.Object;

                if (movies == null)
                    return View("NotFound");


                // Wyszukiwanie
                // szuka w tytułach lub w opisach
                if (!string.IsNullOrEmpty(model.q))
                {
                    movies = movies.Where(
                        w =>
                            w.Title.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                            w.Description.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }


                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Tytuł A-Z":
                        movies = movies.OrderBy(o => o.Title).ToList();
                        break;

                    case "Tytuł Z-A":
                        movies = movies.OrderByDescending(o => o.Title).ToList();
                        break;

                    case "Kategoria A-Z":
                        movies = movies.OrderBy(o => o.Category).ToList();
                        break;

                    case "Kategoria Z-A":
                        movies = movies.OrderByDescending(o => o.Category).ToList();
                        break;
                }

                model.End = model.PageSize + 1;
                int srodek = (int)Math.Round((double)(model.PageSize / 2));

                if (model.PageIndex > srodek)
                {
                    model.Start = model.PageIndex - (srodek - 1);
                    model.End = model.PageIndex + model.PageSize - srodek;
                }

                model.Movies = movies;
                model.PageIndex = Math.Min(model.PageIndex, (int)Math.Ceiling((double)movies.Count / model.PageSize));
                model.Paginator = Paginator<GetMovieDto>.CreateAsync(movies, model.PageIndex, model.PageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, GetMoviesDto model)
        {
            NI.Navigation = Navigation.MoviesIndex;

            try
            {
                var result = await _moviesRepository.GetAll();

                if (result == null || !result.Success)
                    return View("NotFound");

                var movies = result.Object;

                if (movies == null)
                    return View("NotFound");


                // Wyszukiwanie
                // szuka w tytułach lub w opisach
                if (!string.IsNullOrEmpty(model.q))
                {
                    movies = movies.Where(
                        w =>
                            w.Title.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                            w.Description.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }


                // Sortowanie
                switch (model.SortowanieOption)
                {
                    case "Tytuł A-Z":
                        movies = movies.OrderBy(o => o.Title).ToList();
                        break;

                    case "Tytuł Z-A":
                        movies = movies.OrderByDescending(o => o.Title).ToList();
                        break;

                    case "Kategoria A-Z":
                        movies = movies.OrderBy(o => o.Category).ToList();
                        break;

                    case "Kategoria Z-A":
                        movies = movies.OrderByDescending(o => o.Category).ToList();
                        break;
                }

                model.End = model.PageSize + 1;
                int srodek = (int)Math.Round((double)(model.PageSize / 2));

                if (model.PageIndex > srodek)
                {
                    model.Start = model.PageIndex - (srodek - 1);
                    model.End = model.PageIndex + model.PageSize - srodek;
                }

                model.Movies = movies;
                model.PageIndex = Math.Min(model.PageIndex, (int)Math.Ceiling((double)movies.Count / model.PageSize));
                model.Paginator = Paginator<GetMovieDto>.CreateAsync(movies, model.PageIndex, model.PageSize);
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
            NI.Navigation = Navigation.MoviesCreate;
            try
            {
                var result = await _categoriesRepository.GetAll();
                if (result == null || !result.Success)
                    return View("NotFound");

                var categories = result.Object; // pobranie kategorii z obiektu
                if (categories == null)
                    return View("NotFound");

                return View(new CreateMovieDto()
                {
                    CategoriesList = new SelectList(categories, "CategoryId", "Name") // select lista
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMovieDto model)
        {
            NI.Navigation = Navigation.MoviesCreate;
            try
            {
                if (ModelState.IsValid)
                {
                    // przekazanie emaila zalogowanego użytkownika do modelu
                    model.Email = User.Identity.Name;

                    var result = await _moviesRepository.Create(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Movies");


                    // zwraca komunikat błędu związanego z tworzeniem rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }

                model.CategoriesList = new SelectList((await _categoriesRepository.GetAll()).Object, "CategoryId", "Name", model.CategoryId); // select lista
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        [HttpGet]
        public async Task<IActionResult> Edit(string movieId)
        {
            NI.Navigation = Navigation.MoviesEdit;
            try
            {
                if (string.IsNullOrEmpty(movieId))
                    return View("NotFound");

                var result = await _moviesRepository.Get(movieId);

                if (result == null || !result.Success)
                    return View("NotFound");


                var movie = result.Object;
                if (movie == null)
                    return View("NotFound");


                movie.CategoriesList = new SelectList((await _categoriesRepository.GetAll()).Object, "CategoryId", "Name", movie.CategoryId);  // select lista
                return View(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetMovieDto model)
        {
            NI.Navigation = Navigation.MoviesEdit;
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _moviesRepository.Update(new EditMovieDto()
                    {
                        MovieId = model.MovieId,
                        Title = model.Title,
                        Description = model.Description,
                        Photo = model.Photo,
                        PhotoData = model.PhotoData,
                        Price = model.Price,
                        CategoryId = model.CategoryId
                    });
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Movies");


                    // zwraca komunikat błędu związanego z aktualizacją rekordu
                    ViewData["ErrorMessage"] = result.Message;
                }

                model.CategoriesList = new SelectList((await _categoriesRepository.GetAll()).Object, "CategoryId", "Name", model.CategoryId); // select lista
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
            NI.Navigation = Navigation.MoviesDelete;
            try
            {
                if (string.IsNullOrEmpty(id))
                    return View("NotFound");

                var result = await _moviesRepository.Get(id);

                if (result == null || !result.Success)
                    return View("NotFound");


                var category = result.Object;
                if (category == null)
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

                var result = await _moviesRepository.Delete(id);
                if (result == null || !result.Success)
                    return View("NotFound");

                return RedirectToAction("Index", "Movies");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
