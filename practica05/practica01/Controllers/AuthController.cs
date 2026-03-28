using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using practica01.Repositories;

namespace practica01.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel userModel)
        {
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Por favor, ingrese correo y contraseña";
                return View();
            }

            var user = _authRepository.ValidateUser(email, password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
            }

            return View();
        }
    }
}
