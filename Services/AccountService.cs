using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Account;
using WebApplication71.Models;

namespace WebApplication71.Services
{
    public class AccountService //: IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /*
                public async Task<TaskResult<ApplicationUser>> GetUserByEmail(string email)
                {
                    var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser(), Message = "" };

                    try
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                        if (user != null)
                        {
                            taskResult.Success = true;
                            taskResult.Model = user;
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Wskazany adres email nie istnieje";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }
                    return taskResult;
                }


                public async Task<TaskResult<RegisterViewModel>> Register(RegisterViewModel model)
                {
                    var taskResult = new TaskResult<RegisterViewModel>() { Success = true, Model = new RegisterViewModel(), Message = "" };

                    try
                    {
                        var findUser = await _userManager.FindByEmailAsync(model.Email);
                        if (findUser == null)
                        {
                            var user = new ApplicationUser
                            {
                                Id = Guid.NewGuid().ToString(),
                                Email = model.Email,
                                UserName = model.Email,

                                Imie = model.Imie,
                                Nazwisko = model.Nazwisko,
                                Ulica = model.Ulica,
                                NumerUlicy = model.NumerUlicy,
                                Miejscowosc = model.Miejscowosc,
                                Kraj = model.Kraj,
                                KodPocztowy = model.KodPocztowy,
                                DataUrodzenia = model.DataUrodzenia,
                                Telefon = model.Telefon,
                                DataDodania = DateTime.Now.ToString(),

                                ConcurrencyStamp = Guid.NewGuid().ToString(),
                                SecurityStamp = Guid.NewGuid().ToString(),
                                NormalizedEmail = model.Email.ToUpper(),
                                NormalizedUserName = model.Email.ToUpper(),
                                EmailConfirmed = false,
                            };


                            var result = await _userManager.CreateAsync(user, model.Password);

                            if (result.Succeeded)
                            {
                                string token = _userSupportService.GenerateJwtToken(user, model.RoleName);


                                var role = await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.RoleName);
                                if (role != null)
                                {
                                    user.RoleId = role.Id;
                                    await _userManager.UpdateAsync(user);


                                    // dodanie użytkownika do roli 
                                    await _userSupportService.AddToRole(user, role.Id);
                                }



                                taskResult.Success = true;
                                taskResult.Model = new RegisterViewModel()
                                {
                                    Email = user.Email,
                                    Token = token,
                                    RoleName = model.RoleName,
                                };
                                taskResult.Message = "Zarejestrowano";
                            }
                            else
                            {
                                taskResult.Success = false;
                                taskResult.Message = "Nie można zarejestrować użytkownika";
                            }
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Wskazany adres email już istnieje";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return taskResult;
                }
        */



        // Logowanie wyłącznie dla administratora systemu
        /*
                public async Task<TaskResult<LoginViewModel>> Login(LoginViewModel model)
                {
                    var taskResult = new TaskResult<LoginViewModel>() { Success = true, Model = new LoginViewModel(), Message = "" };

                    try
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        if (user != null)
                        {
                            // pobranie wszystkich ról użytkownika
                            var userRoles = await _userManager.GetRolesAsync(user);
                            //string firstRole = (userRoles != null && userRoles.Count > 0) ? userRoles[0] : ""; 

                            for (var i = 0; i < userRoles.Count; i++)
                            {
                                string roleName = userRoles[i];// zalogować się może tylko i wyłącznie administrator
                                if (roleName == "Administrator")
                                {
                                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: true);
                                    if (result.Succeeded)
                                    {
                                        taskResult.Success = true;

                                        DateTime expirationTimeToken = DateTime.Now.AddSeconds(5);
                                        //DateTime expirationTimeNewToken = expirationTimeToken.AddSeconds(5);

                                        taskResult.Model = new LoginViewModel()
                                        {
                                            Email = user.Email,
                                            Token = _userSupportService.GenerateJwtToken(user, roleName),
                                            //NewToken = _userSupportService.GenerateJwtNewToken(model.Email, roleName),
                                            ExpirationTimeToken = expirationTimeToken.ToString(), // czas wylogowania po wygaśnięciu tokena
                                            //ExpirationTimeNewToken = expirationTimeNewToken.ToString(),
                                            Role = roleName,
                                        };


        *//*
                                        // mówi o tym, kiedyu żytkownik się zalogował 
                                        rejestratorLogowaniaId = Guid.NewGuid().ToString();
                                        RejestratorLogowania rejestratorLogowania = new RejestratorLogowania()
                                        {
                                            RejestratorLogowaniaId = rejestratorLogowaniaId,
                                            DataZalogowania = DateTime.Now.ToString(),
                                            DataWylogowania = "",
                                            CzasZalogowania = "",
                                            UserId = user.Id
                                        };
                                        _context.RejestratorLogowania.Add(rejestratorLogowania);
                                        await _context.SaveChangesAsync();
        *//*

                                    }
                                    else
                                    {
                                        taskResult.Success = false;
                                        taskResult.Message = "Błędny login lub hasło";
                                    }
                                    break;
                                }
                                else
                                {
                                    taskResult.Success = false;
                                    taskResult.Message = "Konto dostępne wyłącznie dla administartora systemu";
                                }
                            }
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Użytkownik nie istnieje";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return taskResult;
                }
        */






        /*
                public async Task<ResultViewModel<LoginDto>> Login(LoginDto model)
                {
                    var taskResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto () };

                    try
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        if (user != null)
                        {
                            // pobranie wszystkich ról użytkownika
                            var userRoles = await _userManager.GetRolesAsync(user);
                            if (userRoles.Count > 0)
                            {
                                string firstRole = userRoles[0];
                                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: true);
                                if (result.Succeeded)
                                {
                                    taskResult.Success = true;

                                    // zapisanie daty zalogowania w sesjach
                                    string serializeDataZalogowania = JsonConvert.SerializeObject(DateTime.Now);
                                    _httpContextAccessor.HttpContext?.Session.SetString("dataZalogowania", serializeDataZalogowania);


                                    DateTime expirationTimeToken = DateTime.Now.AddSeconds(600);
                                    DateTime ett = expirationTimeToken;
                                    // czas zamieniony na czas charakterystyczny dla JavaScript w formacie "2024-12-12T11:11:00"
                                    string timeJavaScriptExpirationTimeToken = $"{ett.Year}-{ett.Month}-{ett.Day},{ett.Hour}:{ett.Minute}:{ett.Second}";



                                    taskResult.Object = new LoginDto ()
                                    {
                                        UserId = user.Id,
                                        Email = user.Email,
                                        Token = _userSupportService.GenerateJwtToken(user, firstRole),
                                        //ExpirationTimeToken = expirationTimeToken.ToString(), // czas wylogowania po wygaśnięciu tokena
                                        ExpirationTimeToken = timeJavaScriptExpirationTimeToken,  // czas podany w JavaScript
                                        //ExpirationTimeToken = czasWylogowania.ToString(), // czas wylogowania po wygaśnięciu tokena
                                        Role = firstRole,
                                    };


                                    *//*
                                                                // mówi o tym, kiedyu żytkownik się zalogował 
                                                                rejestratorLogowaniaId = Guid.NewGuid().ToString();
                                                                RejestratorLogowania rejestratorLogowania = new RejestratorLogowania()
                                                                {
                                                                    RejestratorLogowaniaId = rejestratorLogowaniaId,
                                                                    DataZalogowania = DateTime.Now.ToString(),
                                                                    DataWylogowania = "",
                                                                    CzasZalogowania = "",
                                                                    UserId = user.Id
                                                                };
                                                                _context.RejestratorLogowania.Add(rejestratorLogowania);
                                                                await _context.SaveChangesAsync();
                                    *//*
                                }
                                else
                                {
                                    taskResult.Success = false;
                                    taskResult.Message = "Błędny login lub hasło";
                                }
                            }
                            else
                            {
                                taskResult.Success = false;
                                taskResult.Message = "Brak rół użytkownika";
                            }
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Użytkownik nie istnieje";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return taskResult;
                }
        */




        public async Task<ResultViewModel<LoginDto>> Login(LoginDto model)
        {
            var returnResult = new ResultViewModel<LoginDto>() { Success = false, Message = "", Object = new LoginDto() };

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        returnResult.Success = true;

                        returnResult.Object = model;


                        // mówi o tym, kiedyu żytkownik się zalogował 
                        Logowanie logowanie = new Logowanie(
                            userId: user.Id
                            );
                        _context.Logowania.Add(logowanie);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        returnResult.Message = "Błędny login lub hasło";
                    }
                }
                else
                {
                    returnResult.Message = "Użytkownik nie istnieje";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }

            return returnResult;
        }





        /*
                public Task<TaskResult<string>> GenerateNewToken(TokenViewModel model)
                {
                    var taskResult = new TaskResult<string>() { Success = true, Model = "", Message = "" };

                    try
                    {
                        if (model.Email != null && model.Role != null)
                        {
                            var storedRefreshToken = _userSupportService.GenerateJwtNewToken(model.Email, model.Role);
                            if (!string.IsNullOrEmpty(storedRefreshToken))
                            {
                                taskResult.Model = storedRefreshToken;
                                taskResult.Success = true;
                            }
                            else
                            {
                                taskResult.Success = false;
                                taskResult.Message = "Nie można było wygenerować nowego tokena";
                            }
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Email or role was null";
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return Task.FromResult(taskResult);
                }
        */


        /*
                public Task<TaskResult<bool>> TokenTimeExpired ()
                {
                    var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

                    try
                    {
                        if (DateTime.Now >= czasWylogowania)
                        {
                            taskResult.Success = true;
                            taskResult.Model = true;
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Model = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        taskResult.Success = false;
                        taskResult.Message = ex.Message;
                    }

                    return Task.FromResult (taskResult);
                }
        */

        /*
                public async Task Logout()
                {

                    // mówi o tym, kiedyu żytkownik się zalogował i wylogował oraz oblicza czas zalogowania
                    rejestratorLogowaniaId = Guid.NewGuid().ToString();

                    var dataZalogowania = dataZalogowaniaExternal;
                    var dataWylogowania = DateTime.Now.AddSeconds(50);
                    var czasZalogowaniaObliczenia = dataWylogowania - dataZalogowania;
                    var c = czasZalogowaniaObliczenia;
                    TimeSpan czasZalogowania = new TimeSpan(c.Days, c.Hours, c.Minutes, c.Seconds);


                    RejestratorLogowania rejestratorLogowania = new RejestratorLogowania()
                    {
                        RejestratorLogowaniaId = rejestratorLogowaniaId,
                        DataZalogowania = dataZalogowania.ToString(),
                        DataWylogowania = DateTime.Now.ToString(),
                        CzasZalogowania = czasZalogowania.ToString(),
                        UserId = userId
                    };
                    _context.RejestratorLogowania.Add(rejestratorLogowania);
                    _context.SaveChanges();


                    await _signInManager.SignOutAsync();
                }
        */


        /*
                /// <summary>
                /// Logowanie z parametrem, przekazuje czas zalogowania
                /// </summary>
                public async Task Logout(TimeSpanViewModel model)
                {
                    // mówi o tym, kiedyu żytkownik się zalogował i wylogował oraz oblicza czas zalogowania
                    rejestratorLogowaniaId = Guid.NewGuid().ToString();

                    var dataZalogowania = dataZalogowaniaExternal;
                    var dataWylogowania = new DateTime(model.Year, model.Month, model.Day, model.Hour, model.Minute, model.Second);

                    var czasZalogowaniaObliczenia = dataWylogowania - dataZalogowania;
                    var c = czasZalogowaniaObliczenia;
                    //TimeSpan czasZalogowania = new TimeSpan(c.Days, c.Hours, c.Minutes, c.Seconds);

                    //TimeSpan czasZalogowania = new TimeSpan(model.Day, model.Hour, model.Minute, model.Second);
                    //DateTime czasZalogowania = new DateTime(model.Year, model.Month, model.Day, model.Hour, model.Minute, model.Second);


                    RejestratorLogowania rejestratorLogowania = new RejestratorLogowania()
                    {
                        RejestratorLogowaniaId = rejestratorLogowaniaId,
                        DataZalogowania = dataZalogowania.ToString(),
                        DataWylogowania = dataWylogowania.ToString (),
                        CzasZalogowania = czasZalogowaniaObliczenia.ToString(),
                        UserId = userId
                    };
                    _context.RejestratorLogowania.Add(rejestratorLogowania);
                    _context.SaveChanges();


                    await _signInManager.SignOutAsync();
                }
        */

        /*
                public async Task Logout()
                {
                    var logowania = await _context.Logowania
                        .OrderByDescending(o => o.DataLogowania)
                        .ToListAsync();
                    var ostatnieLogowanieUzytkownika =
                    await _signInManager.SignOutAsync();
                }
        */
        /*
                public async Task Logout(string userEmail)
                {
                    // usunięcie pustych rekordów, które nie mają przypisane daty wylogowania
                    var logowaniaUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .Where(w => w.User.Email == userEmail)
                        .ToListAsync();
                    foreach (var log in logowaniaUzytkownika)
                    {
                        if (string.IsNullOrEmpty(log.DataWylogowania))
                        {
                            _context.Logowania.Remove(log);
                            await _context.SaveChangesAsync();
                        }
                    }



                    // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
                    var ostatnieLogowanieUzytkownika = await _context.Logowania
                    .Include(i => i.User)
                    .OrderByDescending(o => o.DataLogowania)
                    .FirstOrDefaultAsync(f => f.User.Email == userEmail);


                    // zapisanie w bazie daty wylogowania użytkownika
                    ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                    _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                    await _context.SaveChangesAsync();




                    List<TimeSpan> czasPracyList = new List<TimeSpan>();
                    var rekordyZalogowanegoUzytkownikaZdniaDzisiejszego = await _context.Logowania
                        .Where(w => w.User.Email == userEmail)
                        .ToListAsync();
                    // przejscie przez wszystkie rekordy
                    foreach (var log in rekordyZalogowanegoUzytkownikaZdniaDzisiejszego)
                    {
                        var dataZalogowania = DateTime.Parse(log.DataLogowania);
                        var dataWylogowania = DateTime.Parse(log.DataWylogowania);
                        var czasPracy = dataWylogowania - dataZalogowania;
                        czasPracyList.Add(czasPracy);
                    }

                    var czasPracySuma = czasPracyList.Aggregate(TimeSpan.Zero, (total, next) => total.Add(next));








                    *//*

                                // zsumowanie dat jeśli użytkownik w ciągu dnia logował się kilkukrotnie
                                // pobranie wszystkich rekordów dla danego użytkownika
                                var logowania = await _context.Logowania
                                    .Include(i => i.User)
                                    .Where(w => w.User.Email == userEmail)
                                    .OrderByDescending(o => o.DataLogowania)
                                    .ToListAsync();
                                // pobranie pierwszego rekordu, na jego podstawie będą przeszukiwane pozostałe zalogowania z jednego dnia o tej samej dacie
                                var firstRecord = logowania.FirstOrDefault();
                                var pobierzLogowaniaZdniaDzisiejszego = logowania.Where(w => w.DataLogowania == firstRecord.DataLogowania).ToList();
                                foreach (var log in pobierzLogowaniaZdniaDzisiejszego)
                                {
                                    // zsumowanie czasu pracy użytkownika w ciągu jednego dnia z kilku rekordów

                                    var dataZalogowania = DateTime.Parse(log.DataLogowania);
                                    var dataWylogowania = DateTime.Parse(log.DataWylogowania);
                                    var czasPracy = dataWylogowania - dataZalogowania;
                                    TimeSpan ts = new TimeSpan(czasPracy.Days, czasPracy.Hours, czasPracy.Minutes, czasPracy.Seconds);

                                    var sumaCzasuPracy = "";
                                }
                    *//*


                    // wylogowanie
                    await _signInManager.SignOutAsync();
                }
        */

        /*
                public async Task Logout(string userEmail)
                {
                    // usunięcie pustych rekordów, które nie mają przypisane daty wylogowania
                    *//*var logowaniaUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .Where(w => w.User.Email == userEmail)
                        .ToListAsync();
                    foreach (var log in logowaniaUzytkownika)
                    {
                        if (string.IsNullOrEmpty(log.DataWylogowania))
                        {
                            _context.Logowania.Remove(log);
                            await _context.SaveChangesAsync();
                        }
                    }
        */

        /*
                    // pobiera wszystkie rekordy z dnia dzisiejszego dla zalogowanego użytkownika aby zsumować je i podliczyć ile łącznie w ciągu całego dnia przepracował
                    var wszystkieRekordy = await _context.Logowania
                        .Include (i=> i.User)
                        .Where (w => w.User.Email == userEmail).ToListAsync ();
                    // rekordy dla dnia dzisiejszego
                    foreach (var rek in wszystkieRekordy)
                    {
                        var dataZalogowania = DateTime.Parse(rek.DataLogowania);
                        var dataWylogowania = DateTime.Parse(rek.DataWylogowania);
                        var czasPracy = dataWylogowania - dataZalogowania; 
                    }
        *//*

        // pobiera wszystkie rekordy z dnia dzisiejszego dla zalogowanego użytkownika aby zsumować je i podliczyć ile łącznie w ciągu całego dnia przepracował
        var wszystkieRekordy = await _context.Logowania
            .Include(i => i.User)
            .Where(w => w.User.Email == userEmail).ToListAsync();
        // rekordy dla dnia dzisiejszego
        if (wszystkieRekordy != null && wszystkieRekordy.Count > 1)
        {
            var pierwszyCzasPracy = wszystkieRekordy[0].DataLogowania;
            var ostatniCzasPracy = wszystkieRekordy[wszystkieRekordy.Count-1].DataLogowania;

            // usunięcie starych rekoród


            var a = DateTime.Parse(pierwszyCzasPracy);
            var b = DateTime.Parse(ostatniCzasPracy);
            var czasPracy = b - a; 

            // utworzenie nowego rekordu czasu pracy
            var logowanie = new Logowanie (
                pierwszyCzasPracy.ToString (),
                ostatniCzasPracy.ToString (),
                czasPracy.ToString (),
                wszystkieRekordy[0].User.Id
                );
            _context.Logowania.Add (logowanie);
        }


*//*
            // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
            var ostatnieLogowanieUzytkownika = await _context.Logowania
                .Include(i => i.User)
                .OrderByDescending(o => o.DataLogowania)
                .FirstOrDefaultAsync(f => f.User.Email == userEmail);

            if (ostatnieLogowanieUzytkownika != null)
            {
                // zapisanie w bazie daty wylogowania użytkownika
                ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
*//*



            // wylogowanie
            await _signInManager.SignOutAsync();
        }
*/



        /*
                public async Task Logout(string userEmail)
                {



                    // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
                    var ostatnieLogowanieUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .OrderByDescending(o => o.DataLogowania)
                        .FirstOrDefaultAsync(f => f.User.Email == userEmail);

                    if (ostatnieLogowanieUzytkownika != null)
                    {
                        // zapisanie w bazie daty wylogowania użytkownika
                        ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                        _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }










                    // Pobiera wszystkie rekordy z dnia dzisiejszego dla zalogowanego użytkownika
                    var dzisiaj = DateTime.Today;

                    var wszystkieRekordy = await _context.Logowania
                        .Include(i => i.User)
                        .Where(w => w.User.Email == userEmail 
                            *//*&& DateTime.Parse(w.DataLogowania).Date == dzisiaj*//* // Filtrujemy tylko dzisiejsze logowania
                            ) 
                        .ToListAsync();

                    // Sprawdzamy, czy są jakiekolwiek rekordy
                    if (wszystkieRekordy != null && wszystkieRekordy.Count > 0)
                    {
                        // Grupujemy według daty logowania
                        var czasPracyDnia = wszystkieRekordy
                            .GroupBy(w => DateTime.Parse(w.DataLogowania).Date)
                            .Select(g => new
                            {
                                Data = g.Key,
                                CzasPracy = g.Sum(x =>
                                {
                                    var logowanie = DateTime.Parse(x.DataLogowania);
                                    var wylogowanie = string.IsNullOrEmpty(x.DataWylogowania)
                                        ? DateTime.Now // Użyj bieżącego czasu, jeśli brak daty wylogowania
                                        : DateTime.Parse(x.DataWylogowania);

                                    return (wylogowanie - logowanie).TotalMinutes;
                                })
                            })
                            .FirstOrDefault();

                        if (czasPracyDnia != null)
                        {
                            // Używamy czasu pracy do utworzenia nowego rekordu
                            var logowanie = new Logowanie(
                                wszystkieRekordy.First().DataLogowania + "_XXX", // Przyjmujemy pierwszy czas logowania
                                wszystkieRekordy.Last().DataWylogowania, // Przyjmujemy ostatni czas wylogowania
                                czasPracyDnia.CzasPracy.ToString(), // Całkowity czas pracy
                                wszystkieRekordy.First().User.Id
                            );

                            _context.Logowania.Add(logowanie);
                            await _context.SaveChangesAsync(); // Zapisujemy zmiany
                        }
                    }






                    // wylogowanie
                    await _signInManager.SignOutAsync();
                }

        */




        /*
                public async Task Logout(string userEmail)
                {
                    // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
                    var ostatnieLogowanieUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .OrderByDescending(o => o.DataLogowania)
                        .FirstOrDefaultAsync(f => f.User.Email == userEmail);

                    if (ostatnieLogowanieUzytkownika != null)
                    {
                        // zapisanie w bazie daty wylogowania użytkownika
                        ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                        _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }








                    var wszystkieRekordy = await _context.Logowania.ToListAsync();

                    var zsumowanyCzas = wszystkieRekordy
                        .GroupBy(w => DateTime.Parse(w.DataLogowania).Date)
                        .Select(g => new
                        {
                            Data = g.Key,
                            CzasPracyMinut = g.Sum(x =>
                            {
                                var logowanie = DateTime.Parse(x.DataLogowania);
                                var wylogowanie = string.IsNullOrEmpty(x.DataWylogowania)
                                    ? DateTime.Now // Używamy bieżącego czasu, jeśli brak daty wylogowania
                                    : DateTime.Parse(x.DataWylogowania);

                                return (wylogowanie - logowanie).TotalMinutes;
                            }),
                            PierwszeLogowanie = g.First().DataLogowania, // Pierwsze logowanie w grupie
                            OstatnieWylogowanie = g.Last().DataWylogowania, // Ostatnie wylogowanie w grupie
                            UserId = g.First().User.Id // Id użytkownika
                        })
                        .ToList();

                    // Dodawanie zsumowanego czasu pracy jako nowych rekordów
                    foreach (var dzien in zsumowanyCzas)
                    {
                        var logowanie = new Logowanie(
                            dzien.PierwszeLogowanie + "_XXX", // Przyjmujemy pierwszy czas logowania + "_XXX"
                            dzien.OstatnieWylogowanie, // Przyjmujemy ostatni czas wylogowania
                            dzien.CzasPracyMinut.ToString(), // Całkowity czas pracy w minutach
                            dzien.UserId // Id użytkownika
                        );

                        _context.Logowania.Add(logowanie);
                    }

                    await _context.SaveChangesAsync(); // Zapisujemy zmiany






                    // wylogowanie
                    await _signInManager.SignOutAsync();
                }
        */





        /*
                public async Task Logout(string userEmail)
                {
                    // wyszukanie wszystkich zalogowań użytkownika, postortowanie ich oraz wybranie ostatniego najnowszego zalogowania
                    var ostatnieLogowanieUzytkownika = await _context.Logowania
                        .Include(i => i.User)
                        .OrderByDescending(o => o.DataLogowania)
                        .FirstOrDefaultAsync(f => f.User.Email == userEmail);

                    if (ostatnieLogowanieUzytkownika != null)
                    {
                        // zapisanie w bazie daty wylogowania użytkownika
                        ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                        _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }








                    var wszystkieRekordy = await _context.Logowania.ToListAsync();

                    var zsumowanyCzas = wszystkieRekordy
                        .GroupBy(w => DateTime.Parse(w.DataLogowania).Date)
                        .Select(g => new
                        {
                            Data = g.Key,
                            CzasPracyMinut = g.Sum(x =>
                            {
                                var logowanie = DateTime.Parse(x.DataLogowania);
                                var wylogowanie = string.IsNullOrEmpty(x.DataWylogowania)
                                    ? DateTime.Now // Używamy bieżącego czasu, jeśli brak daty wylogowania
                                    : DateTime.Parse(x.DataWylogowania);

                                return (wylogowanie - logowanie).TotalMinutes;
                            }),
                            PierwszeLogowanie = g.First().DataLogowania, // Pierwsze logowanie w grupie
                            OstatnieWylogowanie = g.Last().DataWylogowania, // Ostatnie wylogowanie w grupie
                            UserId = g.First().User.Id // Id użytkownika
                        })
                        .ToList();

                    // usuniecie wszystkich pozostałych rekordów i zapisanie nowych
                    *//*foreach (var w in wszystkieRekordy)
                    {
                        _context.Logowania.Remove (w);
                        await _context.SaveChangesAsync ();
                    }*//*

                    // Dodawanie zsumowanego czasu pracy jako nowych rekordów
                    foreach (var dzien in zsumowanyCzas)
                    {
                        var logowanie = new Logowanie(
                            dzien.PierwszeLogowanie, // Przyjmujemy pierwszy czas logowania + "_XXX"
                            dzien.OstatnieWylogowanie, // Przyjmujemy ostatni czas wylogowania
                            dzien.CzasPracyMinut.ToString() + "_XXX", // Całkowity czas pracy w minutach
                            dzien.UserId // Id użytkownika
                        );

                        _context.Logowania.Add(logowanie);
                    }

                    await _context.SaveChangesAsync(); // Zapisujemy zmiany






                    // wylogowanie
                    await _signInManager.SignOutAsync();
                }
        */




        public async Task Logout(string userEmail)
        {

            // jeżeli pole DataWylogowania jest puste oraz rekordów jest więcej niż 1 wtedy one są usuwane, zostaje tylko pierwszy
            var logowaniaUzytkownika = await _context.Logowania
                .Include(i => i.User)
                .OrderByDescending(o => o.DataLogowania)
                .Where(w => w.User.Email == userEmail)
                .ToListAsync();
            if (logowaniaUzytkownika.Count > 1)
            {
                for (var i = 0; i < logowaniaUzytkownika.Count; i++)
                {
                    //pierwszy rekord jest pomijany
                    if (i > 0)
                    {
                        _context.Logowania.Remove(logowaniaUzytkownika[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }





            // wyszukuje najnowszy rekord logowania oraz dopisuje do niego datę wylogowania
            var ostatnieLogowanieUzytkownika = await _context.Logowania
                .Include(i => i.User)
                .OrderByDescending(o => o.DataLogowania)
                .FirstOrDefaultAsync(f => f.User.Email == userEmail);

            if (ostatnieLogowanieUzytkownika != null)
            {
                // zapisanie w bazie daty wylogowania użytkownika
                ostatnieLogowanieUzytkownika.DodajDateWylogowania(DateTime.Now.ToString());
                _context.Entry(ostatnieLogowanieUzytkownika).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }



             


            // wylogowanie
            await _signInManager.SignOutAsync();
        }






        /// <summary>
        /// Oblicza czas pracy dla ostatniego zalogowanego użytkownika uwzględniając datę zalogowania oraz datę wylogowania
        /// </summary>
        private void ObliczCzasPracyDlaOstatniegoZalogowanegoUzytkownika () { }



        /*
                // Usuwa milisekundy z daty, która wygląda np tak "12:12:12.52647568" lub tak "11.12:12:12.52647568"
                private string RemoveMilisecondsFromData (string timeSpanString)
                {
                    string data = "";
                    try
                    {
                        List<string> reverseList = new List<string>();
                        string reverseString = "";

                        // dzieli string na litery i dodaje je do listy
                        for (var i = 0; i < timeSpanString.Length; i++)
                        {
                            reverseList.Add(timeSpanString[i].ToString());
                        }
                        reverseList.Reverse();
                        reverseString = string.Concat(reverseList.ToList());

                        int indexOf = reverseString.IndexOf('.');
                        int index = indexOf + 1;

                        reverseString = reverseString.Substring(index, reverseString.Length - index);
                        reverseList.Clear();
                        for (var i = 0; i < reverseString.Length; i++)
                        {
                            reverseList.Add(timeSpanString[i].ToString());
                        }
                        reverseString = string.Concat(reverseList.ToList());
                        data = reverseString;
                    }
                    catch (Exception ex)
                    {
                        data = "";
                    }
                    return data;
                }
        */

        /*
                public async Task<TaskResult<ApplicationUser>> UpdateAccount(ApplicationUser model)
                {
                    var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser(), Message = "" };

                    try
                    {
                        if (model != null)
                        {
                            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Id == model.Id);
                            if (user != null)
                            {
                                *//*user.Email = model.Email;
                                user.UserName = model.Email;*//*

                                user.Imie = model.Imie;
                                user.Nazwisko = model.Nazwisko;
                                user.Ulica = model.Ulica;
                                user.NumerUlicy = model.NumerUlicy;
                                user.Miejscowosc = model.Miejscowosc;
                                user.KodPocztowy = model.KodPocztowy;
                                user.Kraj = model.Kraj;
                                user.DataUrodzenia = model.DataUrodzenia;
                                user.Telefon = model.Telefon;


                                var userRoles = await _userManager.GetRolesAsync(user);
                                foreach (var role in userRoles)
                                {
                                    await _userSupportService.RemoveFromRole(user, role);
                                }

                                user.RoleId = model.RoleId;
                                var result = await _userManager.UpdateAsync(user);
                                if (result.Succeeded)
                                {
                                    // dodanie zdjęcia
                                    //await CreateNewPhoto (model.Files, user.Id);

                                    *//*
                                                                // Usunięcie ze wszystkich rół
                                                                foreach (var role in await _context.Roles.ToListAsync ())
                                                                    await _userManager.RemoveFromRoleAsync (user, role.Name);


                                                                // Przypisanie nowych ról
                                                                foreach (var selectedRole in model.SelectedRoles)
                                                                {
                                                                    await _userManager.AddToRoleAsync (user, selectedRole);
                                                                    await _userManager.AddClaimAsync (user, new Claim (ClaimTypes.Role, selectedRole));
                                                                }
                                    */


        /*var atr = await AddToRole(user, model.RoleId);
        if (atr)
        {
            taskResult.Success = true;
            taskResult.Model = user;
            taskResult.Message = "Dane zostały zaktualizowane poprawnie";
        }
        else
        {
            taskResult.Success = true;
            taskResult.Model = user;
            taskResult.Message = "Dane zostały zaktualizowane poprawnie ale użytkownik nie został przypisany do roli";
        }*//*


        // dodanie użytkownika do roli
        await _userSupportService.AddToRole(user, model.RoleId);

        taskResult.Success = true;
        taskResult.Model = user;
        taskResult.Message = "Dane zostały zaktualizowane poprawnie";

    }
    else
    {
        taskResult.Success = false;
        taskResult.Message = "Dane nie zostały zaktualizowane";
    }
}
else
{
    taskResult.Success = false;
    taskResult.Message = "User was null";
}
}
else
{
taskResult.Success = false;
taskResult.Message = "Model was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}




public async Task<TaskResult<ChangeEmailViewModel>> ChangeEmail(ChangeEmailViewModel model)
{
var taskResult = new TaskResult<ChangeEmailViewModel>() { Success = true, Model = new ChangeEmailViewModel(), Message = "" };

try
{
// Sprawdza czy pola nie są puste
if (model != null)
{
// wyszukuja użytkownika na podstawie emaila
if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.NewEmail)) == null)
{
    ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
    if (user != null)
    {
        string token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
        var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
        if (result.Succeeded)
        {
            // zaktualizowanie nazwy użytkownika 
            user.Email = model.NewEmail;
            user.UserName = model.NewEmail;
            await _userManager.UpdateAsync(user);
            //await _signInManager.SignOutAsync ();
            taskResult.Success = true;
        }
        else
        {
            taskResult.Success = false;
            taskResult.Message = "Email nie został zmieniony";
        }
    }
}
else
{
    taskResult.Success = false;
    taskResult.Message = "Użytkownik o takim adresie email już istnieje";
}
}
else
{
taskResult.Success = false;
taskResult.Message = "Model was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}
return taskResult;
}



public async Task<TaskResult<ChangePasswordViewModel>> ChangePassword(ChangePasswordViewModel model)
{
var taskResult = new TaskResult<ChangePasswordViewModel>() { Success = true, Model = new ChangePasswordViewModel(), Message = "" };

try
{
if (model != null)
{
ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
if (user != null)
{
    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
    if (result.Succeeded)
    {
        taskResult.Success = true;

        // wylogowanie
        //await _signInManager.SignOutAsync ();
    }
    else
    {
        taskResult.Success = false;
        taskResult.Message = "Błędne hasło";
    }
}
else
{
    taskResult.Success = false;
    taskResult.Message = "Wskazany użytkownik nie istnieje";
}
}
else
{
taskResult.Success = false;
taskResult.Message = "Model was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}







public async Task<TaskResult<bool>> DeleteAccountByUserId(string userId)
{
var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

try
{
if (!string.IsNullOrEmpty(userId))
{
ApplicationUser user = await _userManager.FindByIdAsync(userId);
if (user != null)
{

    *//*
                            // usunięcie zdjęć
                            var photosUser = await _context.PhotosUser.Where (w=> w.UserId == user.Id).ToListAsync ();
                            foreach (var photoUser in photosUser)
                                _context.PhotosUser.Remove (photoUser);
    *//*

    var result = await _userManager.DeleteAsync(user);
    if (result.Succeeded)
    {
        // Wylogowanie użytkownika
        //await _signInManager.SignOutAsync();
        taskResult.Success = true;
    }
}
else
{
    taskResult.Success = false;
    taskResult.Message = "User was null";
}
}
else
{
taskResult.Success = false;
taskResult.Message = "Model was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}





public async Task<TaskResult<bool>> DeleteAccountByEmail(string email)
{
var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

try
{
if (!string.IsNullOrEmpty(email))
{
ApplicationUser user = await _userManager.FindByEmailAsync(email);
if (user != null)
{
    *//*
    // usunięcie zdjęć
    var photosUser = await _context.PhotosUser.Where (w=> w.UserId == user.Id).ToListAsync ();
    foreach (var photoUser in photosUser)
        _context.PhotosUser.Remove (photoUser);
    *//*

    var result = await _userManager.DeleteAsync(user);
    if (result.Succeeded)
    {
        // Wylogowanie użytkownika
        //await _signInManager.SignOutAsync();
        taskResult.Success = false;
    }
}
else
{
    taskResult.Success = false;
    taskResult.Message = "User was null";
}
}
else
{
taskResult.Success = false;
taskResult.Message = "Model was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}





/// <summary>
/// Pobiera wszystkie role danego użytkownika
/// </summary>
public async Task<TaskResult<List<string>>> GetUserRoles(string email)
{
var taskResult = new TaskResult<List<string>>() { Success = true, Model = new List<string>(), Message = "" };

try
{
ApplicationUser user = await _userManager.FindByEmailAsync(email);
if (user != null)
{
var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

taskResult.Success = true;
taskResult.Model = userRoles;
}
else
{
taskResult.Success = false;
taskResult.Message = "User was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}
return taskResult;
}




/// <summary>
/// Pobiera wszystkich użytkowników będących w danej roli
/// </summary>
public async Task<TaskResult<List<ApplicationUser>>> GetUsersInRole(string roleName)
{
var taskResult = new TaskResult<List<ApplicationUser>>() { Success = true, Model = new List<ApplicationUser>(), Message = "" };

try
{
var usersInRole = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
if (usersInRole != null)
{
taskResult.Success = true;
taskResult.Model = usersInRole;
}
else
{
taskResult.Success = false;
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}



/// <summary>
/// Sprawdza czy użytkownik jest w danej roli
/// </summary>
public async Task<TaskResult<bool>> UserInRole(string email, string roleName)
{
var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

try
{
var user = await _userManager.FindByEmailAsync(email);
if (user != null)
{
var userInRole = await _userManager.IsInRoleAsync(user, roleName);
if (userInRole)
{
    taskResult.Success = true;
    taskResult.Model = true;
}
else
{
    taskResult.Success = false;
}
}
else
{
taskResult.Success = false;
taskResult.Message = "User was null";
}

}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}



/// <summary>
/// Sprawdza czy zalogowany user jest administratorem, jeśli tak to przekierowuje go do panelu administratora
/// </summary>
public async Task<TaskResult<bool>> LoggedUserIsAdmin(string email)
{
var taskResult = new TaskResult<bool>() { Success = true, Model = true, Message = "" };

try
{
var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
if (user != null)
{
taskResult.Success = await _userManager.IsInRoleAsync(user, "Administrator");
}
else
{
taskResult.Success = false;
taskResult.Message = "User was null";
}
}
catch (Exception ex)
{
taskResult.Success = false;
taskResult.Message = ex.Message;
}

return taskResult;
}

*/








        private async Task CreateNewPhoto(List<IFormFile> files, string userId)
        {
            /*try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte [] photoData;
                            using (var stream = new MemoryStream ())
                            {
                                file.CopyTo (stream);
                                photoData = stream.ToArray ();

                                PhotoUser photoUser = new PhotoUser ()
                                {
                                    PhotoUserId = Guid.NewGuid ().ToString (),
                                    PhotoData = photoData,
                                    UserId = userId
                                };
                                _context.PhotosUser.Add (photoUser);
                                await _context.SaveChangesAsync ();
                            }
                        }
                    }
                }
            }
            catch { }*/
        }




    }
}
