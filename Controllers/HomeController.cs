using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.Repos.Abs;
using WebApplication71.Services.Abs;

namespace WebApplication71.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;
        private readonly ICategoriesRepository _categoriesRepostiory;
        private readonly IMoviesRepository _moviesRepository;
        private readonly ILogowaniaRepository _logowaniaRepository;
        public Dictionary<string, int> StatystykiDictionary { get; set; }

        public HomeController(IUsersService usersService, IRolesService rolesService, ICategoriesRepository categoriesRepostiory, IMoviesRepository moviesRepository, ILogowaniaRepository logowaniaRepository)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _categoriesRepostiory = categoriesRepostiory;
            _moviesRepository = moviesRepository;
            _logowaniaRepository = logowaniaRepository;


            StatystykiDictionary = new Dictionary<string, int>();
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _usersService.GetAll();
                var roles = await _rolesService.GetAll();
                var categories = await _categoriesRepostiory.GetAll();
                var movies = await _moviesRepository.GetAll();
                var logowania = await _logowaniaRepository.GetAll();

                if (users != null && users.Success &&
                    roles != null && roles.Success &&
                    categories != null && roles.Success &&
                    movies != null && movies.Success &&
                    logowania != null && logowania.Success)
                {

                    StatystykiDictionary = new Dictionary<string, int>()
                    {
                        ["Użytkownicy systemu"] = users.Object.Count,
                        ["Role systemu"] = roles.Object.Count,
                        ["Kategorie systemu"] = categories.Object.Count,
                        ["Filmy systemu"] = movies.Object.Count,
                        ["Logowania użytkowników systemu"] = logowania.Object.Count,
                    };

                    return View(StatystykiDictionary);
                }

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





    }
}
