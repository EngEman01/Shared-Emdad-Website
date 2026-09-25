using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Models.ViewModel;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Admin.Controllers
{
    [Area(SD.Admin_Area)]
    [Authorize(Roles = SD.Role_Admin_Name)]
    public class CategoryTranslationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryTranslationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Create(int categoryId)
        {

            CategoryTranslationVM categoryTranslationVM = new CategoryTranslationVM
            {
                CategoryId = categoryId,

            };
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
            return View(categoryTranslationVM);
        }
        [HttpPost]
        public IActionResult Create(CategoryTranslationVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                return View(model);
            }

            CategoryTranslation categoryTranslation = new CategoryTranslation
            {
                Language = model.CategoryTranslation.Language.ToLower(),
                Name = model.CategoryTranslation.Name,
                Description = model.CategoryTranslation.Description,
                CategoryId = model.CategoryTranslation.CategoryId
            };
            try
            {
                _unitOfWork.CategoryTranslationRepo.Add(categoryTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Translation added successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error adding a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                return View(model);
            }
            var Id = model.CategoryTranslation.CategoryId;
            return RedirectToAction("Show", "Category", new { id = Id });
        }
        public IActionResult Edit(int id)
        {
            var translation = _unitOfWork.CategoryTranslationRepo.Get(t => t.Id == id);
            CategoryTranslationVM categoryTranslationVM = new CategoryTranslationVM
            {
                CategoryId = translation.CategoryId,
                CategoryTranslation = translation

            };
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
            return View(categoryTranslationVM);
        }
        [HttpPost]
        public IActionResult Edit(CategoryTranslationVM model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                return View(model);
            }
            
            

            try
            {
                _unitOfWork.CategoryTranslationRepo.Update(model.CategoryTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Translation updated successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                return View(model);
            }
            var Id = model.CategoryTranslation.CategoryId;
            return RedirectToAction("Show", "Category", new { id = Id });
        }

        public IActionResult Show(int id)
        {
            CategoryTranslation? translation = _unitOfWork.CategoryTranslationRepo.Get(t => t.Id == id);
            return View(translation);
        }

        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }

            CategoryTranslation? translationToBeDeleted = _unitOfWork.CategoryTranslationRepo.Get(t => t.Id == id);
            if (translationToBeDeleted == null)
            {
                return NotFound();
            }

            return View(translationToBeDeleted);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }
            CategoryTranslation? translationToBeDeleted = _unitOfWork.CategoryTranslationRepo.Get(t => t.Id == id);
            int? categoryId = translationToBeDeleted.CategoryId;
            if (translationToBeDeleted != null)
            {
                _unitOfWork.CategoryTranslationRepo.Remove(translationToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
            }
            return RedirectToAction("Show", "Category", new { id = categoryId });
        }

    }
}
