using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.DTOs.Movies;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize(Roles = "Administrator, User")]
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
                return await SearchAndFiltringResutl(model);
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




        private async Task<IActionResult> SearchAndFiltringResutl(GetMoviesDto model)
        {
            if (model == null)
                return View("NotFound");

            var result = await _moviesRepository.GetAll();

            if (result == null || !result.Success)
                return View("NotFound");

            var movies = result.Object;
            if (movies == null)
                return View("NotFound");



            model.ShowPaginator = true;
            model.DisplayNumersListAndPaginatorLinks = true;
            model.DisplayButtonLeftTrzyKropki = false;
            model.DisplayButtonRightTrzyKropki = false;
            model.SortowanieOptionItems = new SelectList(new List<string>() { "Tytuł A-Z", "Tytuł Z-A", "Kategoria A-Z", "Kategoria Z-A", "Cena rosnąco", "Cena malejąco" }, "Tytuł A-Z");



            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
            {
                movies = movies.Where(
                    w =>
                        w.Title.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                        w.Description.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                        ).ToList();

                if (movies.Count < 5)
                {
                    model.DisplayNumersListAndPaginatorLinks = false;
                }
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

                case "Cena rosnąco":
                    movies = movies.OrderBy(o => o.Price).ToList();
                    break;

                case "Cena malejąco":
                    movies = movies.OrderByDescending(o => o.Price).ToList();
                    break;
            }



            model.Movies = movies;
            model.Paginator = Paginator<GetMovieDto>.CreateAsync(movies, model.PageIndex, model.PageSize);





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
            int iloscWszystkichElementow = movies.Count + nextPage;

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
            if (movies.Count <= 5 && model.PageIndex == model.Paginator.TotalPage)
                model.ShowPaginator = false;


            // linki paginacji nie są wyświetlane jeśli ilość elementów na stronie oraz w bazie jest mniejsza od 10
            /*if (roles.Count < 10 && model.PageIndex == 1 && model.Paginator.TotalPage == 1)
                model.DisplayNumersListAndPaginatorLinks = false;*/




            // dlugość paginacji zależna od ilości elementów znajdujących się na liście, im więcej elementów tym więcej elementów w paginacji

            int ilosc = 9;
            if (movies.Count < 10)
                ilosc = 5;
            if (movies.Count > 10 && movies.Count < 50)
                ilosc = 7;
            if (movies.Count > 50)
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


            // jeżeli ktoś w przeglądarce w adresie url wpisze dowolną liczbę w PageSize lub w PageIndex to tu jest przed tym zabezpieczenie
            if (model.PageSize > 20 || model.PageIndex > model.Paginator.TotalPage) 
            {
                model.ShowPaginator = false;
                model.Movies = new List<GetMovieDto>();
                model.Paginator = Paginator<GetMovieDto>.CreateAsync(model.Movies, model.PageIndex, model.PageSize);
            }


            return View(model);
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
                    Title = new Random().Next(1, 10).ToString(),
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
                        Price = model.Price,
                        CategoryId = model.CategoryId,
                        Files = model.Files
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



        [HttpGet]
        public async Task<IActionResult> DeletePhotoMovie(string movieId, string photoMovieId)
        {
            try
            {
                var result = await _moviesRepository.DeletePhotoMovie(photoMovieId);
                return RedirectToAction("Edit", "Movies", new { movieId = movieId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
