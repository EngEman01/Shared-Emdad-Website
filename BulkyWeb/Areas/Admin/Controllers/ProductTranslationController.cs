using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
    public class ProductTranslationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductTranslationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Show(int id)
        {
            ProductTranslation? translation = _unitOfWork.ProductTranslationRepo.Get(t => t.Id == id);
            return View(translation);
        }

        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }

            ProductTranslation? translationToBeDeleted = _unitOfWork.ProductTranslationRepo.Get(t => t.Id == id);
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
            ProductTranslation? translationToBeDeleted = _unitOfWork.ProductTranslationRepo.Get(t => t.Id == id);
            int? productId = translationToBeDeleted.ProductId;

            if (translationToBeDeleted != null)
            {
                _unitOfWork.ProductTranslationRepo.Remove(translationToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Product deleted successfully";
            }
            return RedirectToAction("Show", "Product", new { id = productId });
        }



        public IActionResult Create(int productId)
        {
            if (productId.ToString() == null || productId == 0)
            {
                return NotFound();
            }
            ViewBag.ProductId = productId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductTranslation model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.ProductId = model.ProductId;
                return View(model);
            }
            ProductTranslation productTranslation = new ProductTranslation()
            {
                Language = model.Language,
                Name = model.Name,
                Description = model.Description,
                ProductId = model.ProductId,
            };
            try
            {
                _unitOfWork.ProductTranslationRepo.Add(productTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Translation added successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error adding a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.ProductId = model.ProductId;
                Console.WriteLine(ex.Message);
                return View(model);
            }
            var productId = model.ProductId;
            return RedirectToAction("Show", "Product", new { id = productId });
        }
        
        public IActionResult Edit(int id)
        {
            var translation = _unitOfWork.ProductTranslationRepo.Get(t => t.Id == id);

            ViewBag.ProductId = translation.ProductId;
            ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
            
            return View(translation);
        }
        [HttpPost]
        public IActionResult Edit(ProductTranslation model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.ProductId = model.ProductId;
                return View(model);
            }


            try
            {
                _unitOfWork.ProductTranslationRepo.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Translation updated successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating a translation, please try again.";
                ViewBag.SupportedCultures = _unitOfWork.LocalizationOptions.Value.SupportedCultures;
                ViewBag.ProductId = model.ProductId;
                return View(model);
            }
            var Id = model.ProductId;
            return RedirectToAction("Show", "Product", new { id = Id });
        }

       

       
    }
}
