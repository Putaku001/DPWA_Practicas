using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using practica01.Repositories;

namespace practica01.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            var roles = _roleRepository.GetAllRoles();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.AddRole(role);
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        public IActionResult Edit(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.UpdateRole(role);
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(RoleModel role)
        {
            _roleRepository.DeleteRole(role.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
