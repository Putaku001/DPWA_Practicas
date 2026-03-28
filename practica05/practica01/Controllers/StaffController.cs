using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using practica01.Models;
using practica01.Repositories;
using practica01.Utils;

namespace practica01.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffRepository _staffRepository;
        private readonly StaffCategoryRepository _staffCategoryRepository;
        private readonly SpecialtyRepository _specialtyRepository;
        private readonly IConfiguration _configuration;

        public StaffController(
            StaffRepository staffRepository,
            StaffCategoryRepository staffCategoryRepository,
            SpecialtyRepository specialtyRepository,
            IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _staffCategoryRepository = staffCategoryRepository;
            _specialtyRepository = specialtyRepository;
            _configuration = configuration;
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
            var specialties = _specialtyRepository.GetAll();
            var staffCategories = _staffCategoryRepository.GetAll();

            ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name));
            ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffModel staffModel)
        {
            if (ModelState.IsValid)
            {
                if (staffModel.PhotoFile != null && staffModel.PhotoFile.Length > 0)
                {
                    string? imageUrl = CloudinaryUtil.UploadImage(staffModel.PhotoFile, _configuration);
                    staffModel.PhotoUrl = imageUrl;
                }

                _staffRepository.Add(staffModel);
                return RedirectToAction(nameof(Index));
            }

            var specialties = _specialtyRepository.GetAll();
            var staffCategories = _staffCategoryRepository.GetAll();
            ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name), staffModel.SpecialtyId);
            ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name), staffModel.StaffCategoryId);

            return View(staffModel);
        }

        public IActionResult Edit(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return NotFound();

            var specialties = _specialtyRepository.GetAll();
            var staffCategories = _staffCategoryRepository.GetAll();

            ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name), staff.SpecialtyId);
            ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name), staff.StaffCategoryId);

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffModel staffModel)
        {
            if (ModelState.IsValid)
            {
                if (staffModel.PhotoFile != null && staffModel.PhotoFile.Length > 0)
                {
                    string? imageUrl = CloudinaryUtil.UploadImage(staffModel.PhotoFile, _configuration);
                    staffModel.PhotoUrl = imageUrl;
                }

                _staffRepository.Update(staffModel);
                return RedirectToAction(nameof(Index));
            }

            var specialties = _specialtyRepository.GetAll();
            var staffCategories = _staffCategoryRepository.GetAll();
            ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name), staffModel.SpecialtyId);
            ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name), staffModel.StaffCategoryId);

            return View(staffModel);
        }

        public IActionResult Delete(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(StaffModel staffModel)
        {
            _staffRepository.Delete(staffModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
