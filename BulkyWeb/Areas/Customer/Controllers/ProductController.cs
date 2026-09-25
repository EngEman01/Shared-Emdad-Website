using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StyleHub.DataAccess;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area(SD.Customer_Area)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> Products(int categoryId)
        {

            IEnumerable<Product> products = _unitOfWork.ProductRepo.GetProductsOfCategoryWithSpecificTranslation(getSelectedCulture(), categoryId);
            return View(products);
        }
        public IActionResult Show(int id)
        {
            var product = _unitOfWork.ProductRepo.GetProductInSpecificTranslation(id, getSelectedCulture());
            return View(product);
        }
        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }
    


    }
}
