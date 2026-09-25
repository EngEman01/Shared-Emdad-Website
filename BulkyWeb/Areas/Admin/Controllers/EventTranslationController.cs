using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Admin.Controllers
{
    [Area(SD.Admin_Area)]
    [Authorize(Roles = SD.Role_Admin_Name)]
    public class EventTranslationController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EventTranslationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Show(int id)
        {
            EventTranslation? translation = _unitOfWork.EventTranslationRepo.Get(t => t.Id == id);
            return View(translation);
        }

        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }

            EventTranslation? translationToBeDeleted = _unitOfWork.EventTranslationRepo.Get(t => t.Id == id);
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
            EventTranslation? translationToBeDeleted = _unitOfWork.EventTranslationRepo.Get(t => t.Id == id);
            int? EventId = translationToBeDeleted.EventId;

            if (translationToBeDeleted != null)
            {
                _unitOfWork.EventTranslationRepo.Remove(translationToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Event deleted successfully";
            }
            return RedirectToAction("Show", "Event", new { id = EventId });
        }



        public IActionResult Create(int EventId)
        {
            if (EventId.ToString() == null || EventId == 0)
            {
                return NotFound();
            }
            ViewBag.EventId = EventId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;

            return View();
        }
        [HttpPost]
        public IActionResult Create(EventTranslation model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.EventId = model.EventId;
                return View(model);
            }
            EventTranslation EventTranslation = new EventTranslation()
            {
                Language = model.Language,
                Name = model.Name,
                Description = model.Description,
                EventId = model.EventId,
            };
            try
            {
                _unitOfWork.EventTranslationRepo.Add(EventTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Translation added successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error adding a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.EventId = model.EventId;
                Console.WriteLine(ex.Message);
                return View(model);
            }
            var EventId = model.EventId;
            return RedirectToAction("Show", "Event", new { id = EventId });
        }

        public IActionResult Edit(int id)
        {
            var translation = _unitOfWork.EventTranslationRepo.Get(t => t.Id == id);

            ViewBag.EventId = translation.EventId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;

            return View(translation);
        }
        [HttpPost]
        public IActionResult Edit(EventTranslation model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.EventId = model.EventId;
                return View(model);
            }


            try
            {
                _unitOfWork.EventTranslationRepo.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Translation updated successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.EventId = model.EventId;
                return View(model);
            }
            var Id = model.EventId;
            return RedirectToAction("Show", "Event", new { id = Id });
        }
    }
}
