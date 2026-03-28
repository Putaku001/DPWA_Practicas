using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using practica01.Repositories;

namespace practica01.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly SpecialtyRepository _specialtyRepository;

        public SpecialtyController(SpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public IActionResult Index()
        {
            var specialtyList = _specialtyRepository.GetAll();
            return View(specialtyList);
        }

        public IActionResult Details(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpecialtyModel specialtyModel)
        {
            if (ModelState.IsValid)
            {
                _specialtyRepository.Add(specialtyModel);
                return RedirectToAction(nameof(Index));
            }

            return View(specialtyModel);
        }

        public IActionResult Edit(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpecialtyModel specialtyModel)
        {
            if (ModelState.IsValid)
            {
                _specialtyRepository.Update(specialtyModel);
                return RedirectToAction(nameof(Index));
            }

            return View(specialtyModel);
        }

        public IActionResult Delete(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SpecialtyModel specialtyModel)
        {
            _specialtyRepository.Delete(specialtyModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
