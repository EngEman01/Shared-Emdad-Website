using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.DataAccess;
using StyleHub.Models.ViewModel;
using StyleHub.Models;
using StyleHub.Utility;
using Microsoft.AspNetCore.Authorization;

namespace StyleHubWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin_Name)]
	public class AdvertisementController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private IWebHostEnvironment _webHostEnvironment;
		private readonly ApplicationDbContext _db;

		public AdvertisementController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Advertisement> Ads = _unitOfWork.AdRepo.GetAllAdsWithSpecificTranslation(_unitOfWork._defaultCulture);

			return View(Ads);
		}
		public IActionResult Show(int id)
		{
			Advertisement Advertisement = _unitOfWork.AdRepo.GetAdWithAllTranslations(id);
			return View(Advertisement);
		}

		public IActionResult Upsert(int? id)
		{
			AdVM AdVM = new AdVM();
			//AdVM.Categories = _unitOfWork.CategoryRepo.GetAllCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);

			if (id == null || id == 0)
			{
				// create
				AdVM.Advertisement = new Advertisement();
				AdVM.AdTranslation = new AdTranslation();
			}
			else
			{
				// update
				AdVM.Advertisement = _unitOfWork.AdRepo.GetAdInSpecificTranslation((int)id, _unitOfWork._defaultCulture);
				AdVM.AdTranslation = AdVM.Advertisement.Translations.FirstOrDefault();
			}

			return View(AdVM);

		}
		[HttpPost]
		public IActionResult Upsert(AdVM AdVM, List<IFormFile>? files)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// create
					if (AdVM.Advertisement.Id.ToString() == null || AdVM.Advertisement.Id == 0)
					{
						_unitOfWork.AdRepo.Add(AdVM.Advertisement);
						_unitOfWork.Save();

						AdVM.AdTranslation.AdvertisementId = AdVM.Advertisement.Id;
						_unitOfWork.AdTranslationRepo.Add(AdVM.AdTranslation);
						_unitOfWork.Save();
						TempData["success"] = "Advertisement added successfully!";

						// Save Images
						if (files != null && files.Any())
						{
							AddAdImages(files, AdVM.Advertisement.Id);
						}
					}
					// Update
					else
					{
						// Check if there are new images coming
						if (files != null && files.Any())
						{
							DeleteAdImages(AdVM.Advertisement.Id);
							// Add new images
							AddAdImages(files, AdVM.Advertisement.Id);

						}
						_unitOfWork.AdRepo.Update(AdVM.Advertisement);
						_unitOfWork.AdTranslationRepo.Update(AdVM.AdTranslation);
						_unitOfWork.Save();
						TempData["success"] = "Advertisement updated successfully!";
					}
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

			}
			TempData["error"] = "Error happened, please try again.";
			return RedirectToAction("Upsert", AdVM);

		}

		public IActionResult Delete(int id)
		{
			if (id.ToString() == null || id == 0)
			{
				return NotFound();
			}
			Advertisement AdvertisementToBeDeleted = _unitOfWork.AdRepo.GetAdInSpecificTranslation(id, _unitOfWork._defaultCulture);
			return View(AdvertisementToBeDeleted);
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
			DeleteAdImages(id);

			// delete Advertisement from db
			var AdvertisementToBeDeleted = _unitOfWork.AdRepo.Get(p => p.Id == id);
			if (AdvertisementToBeDeleted != null)
			{
				_unitOfWork.AdRepo.Remove(AdvertisementToBeDeleted);
				_unitOfWork.Save();
			}


			return RedirectToAction("Index", "Advertisement", new { area = "Admin" });
		}



		private void AddAdImages(List<IFormFile> files, int id)
		{
			// save images to folder Advertisements

			List<AdImage> ImagesList = new List<AdImage>();
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			string fileName = "";
			string saveFolderPath = Path.Combine(wwwRootPath, @"images\Advertisements");
			foreach (var file in files)
			{
				fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				using (var fileStream = new FileStream
					(Path.Combine(saveFolderPath, fileName), FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				ImagesList.Add(new AdImage
				{
					AdvertisementId = id,
					ImageURL = Path.Combine(@"images\Advertisements", fileName)
				});

			}
			// Save images to the database
			_unitOfWork.AdImageRepo.AddRange(ImagesList);
			_unitOfWork.Save();

		}

		private void DeleteAdImages(int id)
		{
			// Delete from folder
			List<AdImage> imagesToBeDeleted = _unitOfWork.AdImageRepo
				.Where(x => x.AdvertisementId == id).ToList();
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
			_unitOfWork.AdImageRepo.RemoveRange(imagesToBeDeleted);
			_unitOfWork.Save();

		}
	}
}
