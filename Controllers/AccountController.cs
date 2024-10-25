using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication71.DTOs.Account;
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


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
            => View(new LoginDto() { LoginResult = "" });


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);
                if (result.Success)
                    return RedirectToAction("Index", "Categories");
            }
            return View(model);
        }


        /*
                [AllowAnonymous]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Login(LoginDto model)
                {
                    if (ModelState.IsValid)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                            if (result.Succeeded)
                            {

                                *//*user.IloscZalogowanUpdate(user.IloscZalogowan + 1);


                                await _userManager.UpdateAsync(user);*/
        /*
                                Logowanie logowanie = new Logowanie(
                                    dataLogowania: DateTime.Now,
                                    userId: user.Id
                                    );
                                _context.Logowania.Add(logowanie);
                                await _context.SaveChangesAsync();*//*

                                return RedirectToAction("Index", "Categories");
                            }
                            else
                            {
                                model.LoginResult = "Błędny login lub hasło";
                                return View(model);
                            }
                        }
                        else
                        {
                            model.LoginResult = "User is null";
                        }
                    }
                    return View(model);
                }*/



        /*
                public async Task<IActionResult> Login(LoginDto model)
                {
                    if (ModelState.IsValid)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                            if (result.Succeeded)
                            {

                                user.IloscZalogowanUpdate(user.IloscZalogowan + 1);
                                await _userManager.UpdateAsync(user);

                                Logowanie logowanie = new Logowanie(
                                    dataLogowania: DateTime.Now,
                                    userId: user.Id
                                    );
                                _context.Logowania.Add(logowanie);
                                await _context.SaveChangesAsync();

                                return RedirectToAction("Index", "Categories");
                            }
                            else
                            {
                                model.LoginResult = "Błędny login lub hasło";
                                return View(model);
                            }
                        }
                    }
                    return View (model);
                }
        */




        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // zalogowany użytkownik
            string email = HttpContext.User.Identity.Name;

            await _accountService.Logout(email);
            return RedirectToAction("Index", "Home");
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


    }
}
