using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using practica01.Repositories;
using System.Security.Claims;

namespace practica01.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(UserModel userModel)
        {
            userModel.RoleId = 2;
            ModelState.Remove(nameof(UserModel.Role));

            if (ModelState.IsValid)
            {
                bool isRegistered = _authRepository.RegisterUser(userModel);

                if (isRegistered)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewBag.Error = "Registration failed. Please try again.";
                }
            }

            return View(userModel);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Por favor, ingrese correo y contraseña";
                return View();
            }

            var user = _authRepository.ValidateUser(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id.ToString())
                };

                if (!string.IsNullOrWhiteSpace(user.Role?.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
