using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleHub.DataAccess;
using StyleHub.DataAccess.Repository;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area(SD.Customer_Area)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public CategoryController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            this._unitOfWork = unitOfWork;
            this._context = context;
        }
        // View All Parent Categories
        public async Task<IActionResult> Categories()
        {
            // Get the selected culture
            var selectedCulture = getSelectedCulture().ToLower();

            var categories = _unitOfWork.CategoryRepo.GetAllParentCategoriesWithSpecificCategoryTranslation(selectedCulture);
            

            return View(categories);
        }
        public IActionResult SubCategories(int categoryId)
        {
            var culture = getSelectedCulture().ToLower();

            // If no categoryId provided or categoryId <= 0, display all subcategories so page is never empty
            if (categoryId <= 0)
            {
                var allSubCategories = _unitOfWork.CategoryRepo.GetAllSubCategoreisWithSpecificCategoryTranslation(culture);
                if (allSubCategories != null && allSubCategories.Any())
                {
                    ViewBag.CategoryId = 0;
                    ViewBag.CategoryTitle = culture == "ar" ? "كافة الأقسام الفرعية" : "All Subcategories";
                    return View(allSubCategories);
                }
                return RedirectToAction("Categories");
            }

            var subCategories = _unitOfWork.CategoryRepo.GetSubCategoreisWithSpecificCategoryTranslation(categoryId: categoryId, culture);
            if (subCategories != null && subCategories.Any())
            {
                var parentCat = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation(categoryId, culture);
                var parentName = parentCat?.CategoryTranslations?.FirstOrDefault()?.Name;
                ViewBag.CategoryId = categoryId;
                ViewBag.CategoryTitle = parentName;
                return View(subCategories);
            }

            // else get the products of it in the sub category
            return RedirectToAction("Products", "Product", new { Area = "Customer", categoryId = categoryId });
        }

        public IActionResult SubSubCategories(int categoryId)
        {
            var culture = getSelectedCulture().ToLower();

            if (categoryId <= 0)
            {
                return RedirectToAction("Categories");
            }

            var subSubCategories = _unitOfWork.CategoryRepo.GetSubCategoreisWithSpecificCategoryTranslation(categoryId: categoryId, culture);
            if (subSubCategories != null && subSubCategories.Any())
            {
                var parentCat = _unitOfWork.CategoryRepo.GetCategoryWithSpecificCategoryTranslation(categoryId, culture);
                var parentName = parentCat?.CategoryTranslations?.FirstOrDefault()?.Name;
                ViewBag.CategoryId = categoryId;
                ViewBag.CategoryTitle = parentName;
                return View(subSubCategories);
            }

            // else return the products in the sub sub category
            return RedirectToAction("Products", "Product", new { Area = "Customer", categoryId = categoryId });
        }

        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }


    }
    
    }
