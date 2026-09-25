using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Admin.Controllers
{
    [Area(SD.Admin_Area)]
    [Authorize(Roles = SD.Role_Admin_Name)]
    public class AdTranslationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdTranslationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Show(int id)
        {
            AdTranslation? translation = _unitOfWork.AdTranslationRepo.Get(t => t.Id == id);
            return View(translation);
        }

        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }

            AdTranslation? translationToBeDeleted = _unitOfWork.AdTranslationRepo.Get(t => t.Id == id);
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
            AdTranslation? translationToBeDeleted = _unitOfWork.AdTranslationRepo.Get(t => t.Id == id);
            int? AdvertisementId = translationToBeDeleted.AdvertisementId;

            if (translationToBeDeleted != null)
            {
                _unitOfWork.AdTranslationRepo.Remove(translationToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Advertisement deleted successfully";
            }
            return RedirectToAction("Show", "Advertisement", new { id = AdvertisementId });
        }



        public IActionResult Create(int AdvertisementId)
        {
            if (AdvertisementId.ToString() == null || AdvertisementId == 0)
            {
                return NotFound();
            }
            ViewBag.AdvertisementId = AdvertisementId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;

            return View();
        }
        [HttpPost]
        public IActionResult Create(AdTranslation model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.AdvertisementId = model.AdvertisementId;
                return View(model);
            }
            AdTranslation AdTranslation = new AdTranslation()
            {
                Language = model.Language,
                Name = model.Name,
                Description = model.Description,
                AdvertisementId = model.AdvertisementId,
            };
            try
            {
                _unitOfWork.AdTranslationRepo.Add(AdTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Translation added successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error adding a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.AdvertisementId = model.AdvertisementId;
                Console.WriteLine(ex.Message);
                return View(model);
            }
            var AdvertisementId = model.AdvertisementId;
            return RedirectToAction("Show", "Advertisement", new { id = AdvertisementId });
        }

        public IActionResult Edit(int id)
        {
            var translation = _unitOfWork.AdTranslationRepo.Get(t => t.Id == id);

            ViewBag.AdvertisementId = translation.AdvertisementId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;

            return View(translation);
        }
        [HttpPost]
        public IActionResult Edit(AdTranslation model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.AdvertisementId = model.AdvertisementId;
                return View(model);
            }


            try
            {
                _unitOfWork.AdTranslationRepo.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Translation updated successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.AdvertisementId = model.AdvertisementId;
                return View(model);
            }
            var Id = model.AdvertisementId;
            return RedirectToAction("Show", "Advertisement", new { id = Id });
        }
    }
}
