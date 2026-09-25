using StyleHub.DataAccess;
using StyleHub.DataAccess.Repository;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using StyleHub.Models.ViewModel;

namespace StyleHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin_Name)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var allCategories = _unitOfWork.CategoryRepo.GetAllCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);

            return View(allCategories);

        }
        public IActionResult ParentCategories()
        {
            var parentCategories = _unitOfWork.CategoryRepo.GetAllParentCategoriesWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);
            return View(parentCategories);
        }
        public IActionResult SubCategories()
        {
            var subCategories = _unitOfWork.CategoryRepo.GetAllSubCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);
            return View(subCategories);
        }
        public IActionResult Show(int id)
        {
            // REPO: Get one category with all its category translations

            var category = _unitOfWork.CategoryRepo.GetCategoryWithAllCategoryTranslation(id);
            var categoryShowVM = new CategoryShowVM
            {
                Category = category,
                CategoryTranslations = category.CategoryTranslations,
                SubCategories = _unitOfWork.CategoryRepo.GetSubCategoreisWithSpecificCategoryTranslation(id, _unitOfWork._defaultCulture)
            };

            return View(categoryShowVM);
        }
        public IActionResult Create(int? parentCategoryId)
        {
            var allCategories = _unitOfWork.CategoryRepo.GetAllCategoreisWithAllCategoryTranslations();
            ViewBag.Categories = allCategories;
            if(parentCategoryId != null)
            {
                var parentCategory = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation((int) parentCategoryId, _unitOfWork._defaultCulture);
                ViewBag.ParentCategory = parentCategory;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryVM categoryCreateVM, IFormFile? file)
        {

            if (file != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string saveFolderPath = Path.Combine(wwwRootPath, @"images/categories");
                using (var fileStream = new FileStream(Path.Combine(saveFolderPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                categoryCreateVM.Category.ImageUrl = @"images/categories/" + fileName;
            }


            var categoryTranslation = new CategoryTranslation
            {
                Language = _unitOfWork._defaultCulture,
                Name = categoryCreateVM.CategoryTranslation.Name,
                Description = categoryCreateVM.CategoryTranslation.Description,
            };
            if(categoryCreateVM.Category.ParentCategoryId == 0)
            {
                categoryCreateVM.Category.ParentCategoryId = null;
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(categoryCreateVM.Category);
                _unitOfWork.Save();
                categoryTranslation.CategoryId = categoryCreateVM.Category.Id;

                _unitOfWork.CategoryTranslationRepo.Add(categoryTranslation); 
                _unitOfWork.Save();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View();


        }
        public  IActionResult Edit(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }
            var categoryEditVM = new CategoryEditVM
            {
                Category = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation(id, _unitOfWork._defaultCulture),
                CategoryTranslation = _unitOfWork.CategoryTranslationRepo.Get( t => t.CategoryId == id && t.Language.ToLower() == _unitOfWork._defaultCulture.ToLower()),
            };


            var categories = _unitOfWork.CategoryRepo.GetAllCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture).Where(c => c.Id != id);
            ViewBag.Categories = categories;
           
            return View(categoryEditVM);
        }
        [HttpPost]
        public IActionResult Edit(CategoryEditVM model, IFormFile? file)
        {
            var categoryFromDb = _unitOfWork.CategoryRepo.Get(c => c.Id == model.Category.Id);
            // Update Category Image
            if (file != null)
            {
                // Delete old image if exists
                string wwwRootPath = _webHostEnvironment.WebRootPath;


                if (!categoryFromDb.ImageUrl.IsNullOrEmpty())
                {
                    string oldImagePath = Path.Combine(wwwRootPath, categoryFromDb.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save the new image
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string saveFolderPath = Path.Combine(wwwRootPath, @"images/categories");
                using (var fileStream = new FileStream(Path.Combine(saveFolderPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                categoryFromDb.ImageUrl = @"images/categories/" + fileName;
                


            }
            categoryFromDb.ParentCategoryId = model.Category.ParentCategoryId;

            // Update Category in db when valid
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Update(categoryFromDb);
                _unitOfWork.CategoryTranslationRepo.Update(model.CategoryTranslation);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation(id, _unitOfWork._defaultCulture);
            

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation(id, _unitOfWork._defaultCulture);


            if (category != null)
            {
                // Set the ParentCategoryId to each of its clinet categories to null manually
                try
                {
                    var subcategories = _unitOfWork.CategoryRepo.Where(c => c.ParentCategoryId == category.Id);
                    if (subcategories != null && subcategories.Any())
                    {
                        foreach (var cat in subcategories)
                        {
                            cat.ParentCategoryId = null;
                            _unitOfWork.CategoryRepo.Update(cat);
                        }
                    }
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Couldnot update the subcategories of the category to be deleted");
                }

                // Delete old image if exists
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (!category.ImageUrl.IsNullOrEmpty())
                {
                    string oldImagePath = Path.Combine(wwwRootPath, category.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.CategoryRepo.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
            }
           
            return RedirectToAction("Index");
        }

        //#region API CALLS
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    List<Category> Categories = _unitOfWork.CategoryRepo.GetAll().ToList();
        //    return Json(new {data  = Categories});
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{

        //    if(id.ToString() == null || id == 0)
        //    {
        //        return Json(new {success = false, message = "Error happened while deleting the category!"});
        //    }



        //    Category objectToBeDeleted = _unitOfWork.CategoryRepo.Get(o => o.Id == id);
        //    string imagePath = _webHostEnvironment.WebRootPath + objectToBeDeleted.ImageUrl; 
        //    if (imagePath != null && System.IO.File.Exists(imagePath))
        //    {
        //        System.IO.File.Delete(imagePath);
        //    }
        //    _unitOfWork.CategoryRepo.Remove(id);
        //    _unitOfWork.Save();

        //    return Json(new { success = true, message= "Category Deleted successfully!" });
        //}


        //#endregion


    }
}
