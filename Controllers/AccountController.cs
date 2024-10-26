using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication71.DTOs.Account;
using WebApplication71.DTOs.Users;
using WebApplication71.Services;

namespace WebApplication71.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /*
                [AllowAnonymous]
                [HttpGet]
                public IActionResult Register()
                    => View(new RegisterViewModel() { RegisterResult = "" });

                [AllowAnonymous]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Register(RegisterViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        ApplicationUser user = new ApplicationUser(
                            email: model.Email,
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            photo: model.Photo,
                            telefon: model.Telefon,
                            dataUrodzenia: model.DataUrodzenia,
                            ulica: model.Ulica,
                            pesel: model.Pesel,
                            miejscowosc: model.Miejscowosc,
                            wojewodztwo: model.Wojewodztwo,
                            plec: model.Plec
                            );

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            // dodanie nowozarejestrowanego użytkownika do roli 
                            await _userManager.AddToRoleAsync(user, "User");
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));

                            // zalogowanie
                            await _signInManager.SignInAsync(user, false);
                            model.RegisterResult = "Zarejestrowano, sprawdź pocztę aby dokończyć rejestrację";
                        }
                        else
                        {
                            model.RegisterResult = "Nie zarejestrowano";
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    return View(model);
                }


                [HttpGet]
                public IActionResult ChangePassword()
                {
                    return View(new ChangePasswordViewModel() { ChangePasswordResult = "" });
                }

                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                        if (user == null)
                        {
                            model.ChangePasswordResult = "Wskazany użytkownik nie istnieje";
                        }
                        else
                        {
                            if (model.OldPassword != model.NewPassword)
                            {
                                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                                if (result.Succeeded)
                                {
                                    model.ChangePasswordResult = "Hasło zmienione poprawnie";
                                    //await _signInManager.SignOutAsync ();
                                    //return RedirectToAction ("Login", "Home");
                                }
                            }
                            else
                            {
                                model.ChangePasswordResult = "Hasła różnią się od siebie";
                            }

                        }
                    }
                    return View(model);
                }


                public async Task<IActionResult> DeleteAccount()
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (user != null)
                    {
                        IdentityResult result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        // model.Result = "Wskazany użytkownik nie istnieje";
                    }
                    return RedirectToAction("Index", "Home");
                }


                [HttpGet]
                public IActionResult ForgotPassword()
                    => View();

                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ForgotPassword(string email)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                        return RedirectToAction("ForgotPasswordConfirmation");

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account", new { email, token }, Request.Scheme);

                    return RedirectToAction("ForgotPasswordConfirmation");
                }


                [HttpGet]
                public IActionResult ResetPassword(string email, string token)
                {
                    return View(new ResetPasswordDto()
                    {
                        Email = email,
                        Token = token
                    });
                }

                [HttpPost]
                public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction("ResetPasswordConfirmation");
                    else
                        return View(model);
                }
        */




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


                return View(new GetUserDto()
                {
                    Email = user.Email,
                    Imie = user.Imie,
                    Nazwisko = user.Nazwisko,
                    Ulica = user.Ulica,
                    Miejscowosc = user.Miejscowosc,
                    Wojewodztwo = user.Wojewodztwo,
                    KodPocztowy = user.KodPocztowy,
                    Pesel = user.Pesel,
                    DataUrodzenia = user.DataUrodzenia,
                    Plec = user.Plec,
                    Telefon = user.Telefon,
                    Photo = user.Photo
                });
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
                        Email = model.Email,
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
                        Photo = model.Photo
                    });
                    if (result != null && result.Success)
                        return RedirectToAction("Edit", "Account");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
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
                        return RedirectToAction("Index", "Home");
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
        {
            return View();
        }


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
                        return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet]
        public IActionResult DeleteAccount(string id)
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
        public async Task<IActionResult> DeleteAccountConfirmed(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return View("NotFound");

                await _accountService.DeleteAccountByEmail(User.Identity.Name);

                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
            => View(new LoginDto() { LoginResult = "" });


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
                    if (result.Success)
                        return RedirectToAction("Index", "Home");
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
