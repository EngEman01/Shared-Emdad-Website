using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Models.ViewModel;
using StyleHub.Utility;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StyleHub.DataAccess;
using Microsoft.Identity.Client;
using StyleHub.DataAccess.Repository;



namespace StyleHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin_Name)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> Products = _unitOfWork.ProductRepo.GetAllProductsWithSpecificTranslation(_unitOfWork._defaultCulture);

            return View(Products);
        }
        public IActionResult Show(int id)
        {
            Product product = _unitOfWork.ProductRepo.GetProductWithAllTranslations(id);
            return View(product);
        }
      
        public IActionResult Upsert(int? id)
        {

            ProductVM ProductVM = new ProductVM();
            ProductVM.Categories = _unitOfWork.CategoryRepo.GetAllCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);

            if (id == null || id == 0)
            {
                // create
                ProductVM.Product = new Product();
                ProductVM.ProductTranslation = new ProductTranslation();
            }
            else
            {
                // update
                ProductVM.Product = _unitOfWork.ProductRepo.GetProductInSpecificTranslation((int) id, _unitOfWork._defaultCulture);
                ProductVM.ProductTranslation = ProductVM.Product.ProductTranslations.FirstOrDefault();
            }

            return View(ProductVM);

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // create
                    if (ProductVM.Product.Id.ToString() == null || ProductVM.Product.Id == 0)
                    {
                        _unitOfWork.ProductRepo.Add(ProductVM.Product);
                        _unitOfWork.Save();
                        
                        ProductVM.ProductTranslation.ProductId = ProductVM.Product.Id;
                        _unitOfWork.ProductTranslationRepo.Add(ProductVM.ProductTranslation);
                        _unitOfWork.Save();
                        TempData["success"] = "Product added successfully!";
                        // Save Images
                        if (files != null && files.Any())
                        {
                            AddProductImages(files, ProductVM.Product.Id);
                        }
                    }
                    // Update
                    else
                    {
                        // Check if there are new images coming
                        if (files != null && files.Any())
                        {
                            DeleteProductImages(ProductVM.Product.Id);
                            // Add new images
                            AddProductImages(files, ProductVM.Product.Id);

                        }
                        _unitOfWork.ProductRepo.Update(ProductVM.Product);
                        _unitOfWork.ProductTranslationRepo.Update(ProductVM.ProductTranslation);
                        _unitOfWork.Save();
                        TempData["success"] = "Product updated successfully!";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            TempData["error"] = "Error happened, please try again.";
            return RedirectToAction("Upsert", ProductVM );

        }

        public IActionResult Delete(int id)
        {
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }
            Product productToBeDeleted = _unitOfWork.ProductRepo.GetProductInSpecificTranslation(id, _unitOfWork._defaultCulture);
            return View(productToBeDeleted);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            // validate
            if (id.ToString() == null || id == 0)
            {
                return NotFound();
            }
            // delete images in wwwroot
            DeleteProductImages(id);

            // delete product from db
            var productToBeDeleted = _unitOfWork.ProductRepo.Get(p => p.Id == id);
            if (productToBeDeleted != null)
            {
                _unitOfWork.ProductRepo.Remove(productToBeDeleted);
                _unitOfWork.Save();
            }


            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }



        private void AddProductImages(List<IFormFile> files, int id)
        {
            // save images to folder products

            List<ProductImage> ImagesList = new List<ProductImage>();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = "";
            string saveFolderPath = Path.Combine(wwwRootPath, @"images\products");
            foreach (var file in files)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream
                    (Path.Combine(saveFolderPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                ImagesList.Add(new ProductImage
                {
                    ProductId = id,
                    ImageURL = Path.Combine(@"images\products", fileName)
                });

            }
            // Save images to the database
            _unitOfWork.ProductImageRepo.AddRange(ImagesList);
            _unitOfWork.Save();

        }

        private void DeleteProductImages(int id)
        {
            // Delete from folder
            List<ProductImage> imagesToBeDeleted = _unitOfWork.ProductImageRepo
                .Where(x => x.ProductId == id).ToList();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (!imagesToBeDeleted.IsNullOrEmpty())
            {
                string imagePath;
                foreach (var image in imagesToBeDeleted)
                {
                    imagePath = Path.Combine(wwwRootPath, image.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }

            // Delete from database
            _unitOfWork.ProductImageRepo.RemoveRange(imagesToBeDeleted);
            _unitOfWork.Save();

        }

        //public IActionResult Edit()
        //{
        //    return View();
        //}
        //public IActionResult Create()
        //{
        //    ViewBag.Categories = _unitOfWork.CategoryRepo.Get(c => c.ParentCategoryId == null);
        //    ViewBag.SubCategories = _unitOfWork.CategoryRepo.Get(c => c.ParentCategoryId != null);
        //    return View();
        //}

        //public async Task<IActionResult> Create(Product product, List<IFormFile> ProductImages)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Add product to the database
        //        _unitOfWork.ProductRepo.Add(product);
        //        _unitOfWork.Save();

        //        // If there are images, add them
        //        if (ProductImages != null && ProductImages.Count > 0)
        //        {
        //            AddAdvertisementImages(ProductImages, product.Id);  // Save images and update product with image paths
        //        }

        //        return RedirectToAction(nameof(Index));  // Redirect to list page after saving
        //    }

        //    return View(product);  // Return to the view with validation errors if model is invalid
        //}


        //#region APICALLS

        //public IActionResult GetAll()
        //{
        //    List<Product> Products = _unitUnitOfWork.ProductRepo.GetAll(includeProperties : "Category").ToList();
        //    return Json(new { data = Products });
        //}

        //public IActionResult Delete(int id)
        //{
        //    if (string.Equals(id.ToString(), null) || id == 0)
        //    {
        //        return Json(new { success = false, message = "Error happened, Please try again." });
        //    }


        //    // Delete product from db
        //    _unitUnitOfWork.ProductRepo.Remove(id);

        //    // Remove its images
        //    DeleteAdvertisementImages(id);

        //    return Json(new {success = true, message = "Product Deleted successfully"});
        //}
        //#endregion
    }
}
