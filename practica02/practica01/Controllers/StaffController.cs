using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using practica01.Repositories;

namespace practica01.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffRepository _staffRepository;

        public StaffController(StaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public IActionResult Index()
        {
            var staffList = _staffRepository.GetAll();
            return View(staffList);
        }

        public IActionResult Details(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffModel staffModel)
        {
            if (ModelState.IsValid)
            {
                _staffRepository.Add(staffModel);
                return RedirectToAction(nameof(Index));
            }

            return View(staffModel);
        }

        public IActionResult Edit(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffModel staffModel)
        {
            if (ModelState.IsValid)
            {
                _staffRepository.Update(staffModel);
                return RedirectToAction(nameof(Index));
            }

            return View(staffModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _staffRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
