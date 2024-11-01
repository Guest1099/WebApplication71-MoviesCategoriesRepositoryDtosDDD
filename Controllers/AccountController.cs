using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication71.DTOs.Account;
using WebApplication71.DTOs.Users;
using WebApplication71.Services.Abs;

namespace WebApplication71.Controllers
{
    /// <summary>
    /// Akcje dla zalogowanego użytkownika
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var result = await _accountService.GetUserByEmail(User.Identity.Name);
                if (result == null || !result.Success)
                    return View("NotFound");

                var user = result.Object;
                if (user == null)
                    return View("NotFound");


                return View(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetUserDto model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email))
                    return View("NotFound");


                if (ModelState.IsValid)
                {
                    var result = await _accountService.UpdateAccount(new UpdateAccountDto()
                    {
                        Id = model.Id,
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        Ulica = model.Ulica,
                        Miejscowosc = model.Miejscowosc,
                        Wojewodztwo = model.Wojewodztwo,
                        KodPocztowy = model.KodPocztowy,
                        Pesel = model.Pesel,
                        DataUrodzenia = model.DataUrodzenia,
                        Plec = model.Plec,
                        Telefon = model.Telefon,
                        Photo = model.Photo,
                        PhotoData = model.PhotoData
                    });
                    if (result != null)
                    {
                        if (result.Success)
                            return RedirectToAction("Edit", "Account");


                        ViewData["ErrorMessage"] = result.Message;
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        public async Task<IActionResult> ChangeEmail()
        {
            try
            {
                var result = await _accountService.GetUserByEmail(User.Identity.Name);
                if (result == null || !result.Success)
                    return View("NotFound");

                var user = result.Object;
                if (user == null)
                    return View("NotFound");


                return View(new ChangeEmailDto()
                {
                    Email = user.Email,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Email = User.Identity.Name;
                    var result = await _accountService.ChangeEmail(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Logout", "Account");


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
        public IActionResult ChangePassword()
            => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Email = User.Identity.Name;
                    var result = await _accountService.ChangePassword(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Logout", "Account");


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







        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
            => View();


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.ForgotPassword(model);
                    /*if (result != null && result.Success)
                        return RedirectToAction("ForgotPassword", "Account");*/

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




        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
            => View(new ResetPasswordDto()
            {
                //Email = "admin@admin.pl",
                Email = email, // tu musi być prawdziwy adres email
                Token = token,
            });


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.ResetPassword(model);

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

                var result = await _accountService.DeleteAccountByEmail(User.Identity.Name);
                if (result != null && result.Success)
                    return RedirectToAction("Index", "Account");

                return RedirectToAction("Delete", "Account", new { id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
            => View();


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Login(model);
                    if (result != null && result.Success)
                        return RedirectToAction("Index", "Home");


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
        public async Task<IActionResult> Logout()
        {
            try
            {
                // zalogowany użytkownik
                string email = HttpContext.User.Identity.Name;

                await _accountService.Logout(email);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
