using Application.Services.Abs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.Models;
using WebApplication71.Models.Enums;
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

        private List<StatystykiViewModel> _statystyki;

        public HomeController(IUsersService usersService, IRolesService rolesService, ICategoriesRepository categoriesRepostiory, IMoviesRepository moviesRepository, ILogowaniaRepository logowaniaRepository)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _categoriesRepostiory = categoriesRepostiory;
            _moviesRepository = moviesRepository;
            _logowaniaRepository = logowaniaRepository;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            NI.Navigation = Navigation.HomeIndex;

            try
            {
                var users = await _usersService.GetAll();
                var roles = await _rolesService.GetAll();
                var categories = await _categoriesRepostiory.GetAll();
                var movies = await _moviesRepository.GetAll();

                string email = "";
                if (User != null && User.Identity != null)
                    email = User.Identity.Name;

                var logowania = await _logowaniaRepository.GetAll(email);

                if (users != null && users.Success &&
                    roles != null && roles.Success &&
                    categories != null && roles.Success &&
                    movies != null && movies.Success &&
                    logowania != null && logowania.Success)
                {

                    _statystyki = new List<StatystykiViewModel>()
                    {
                        new StatystykiViewModel ()
                        {
                            TitleDisplay = "Użytkownicy systemu",
                            IloscElementow = users.Object.Count,
                            Controller = "Users"
                        },
                        new StatystykiViewModel ()
                        {
                            TitleDisplay = "Role systemu",
                            IloscElementow = roles.Object.Count,
                            Controller = "Roles"
                        },
                        new StatystykiViewModel ()
                        {
                            TitleDisplay = "Kategorie systemu",
                            IloscElementow = categories.Object.Count,
                            Controller = "Categories"
                        },
                        new StatystykiViewModel ()
                        {
                            TitleDisplay = "Filmy systemu",
                            IloscElementow = movies.Object.Count,
                            Controller = "Movies"
                        },
                        new StatystykiViewModel ()
                        {
                            TitleDisplay = "Logowania użytkowników systemu",
                            IloscElementow = logowania.Object.Count,
                            Controller = "Logowania"
                        },
                    };

                    return View(_statystyki);
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
