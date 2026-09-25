using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.DataAccess;
using StyleHub.Models.ViewModel;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin_Name)]
	public class EventController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		private IWebHostEnvironment _webHostEnvironment;
		private readonly ApplicationDbContext _db;

		public EventController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_db = db;
		}

        //public IActionResult Index()
        //{
        //	IEnumerable<Event> Events = _unitOfWork.EventRepo.GetAllEventsWithSpecificTranslation(_unitOfWork._defaultCulture);

        //	return View(Events);
        //}


        public IActionResult Index()
        {
            var events = _unitOfWork.EventRepo
                .GetAllEventsWithSpecificTranslation(_unitOfWork._defaultCulture)
                .Where(e => e.Id != 13) // استبعاد الحدث 13
                .ToList();

            return View(events);
        }

        //      public IActionResult Show(int id)
        //{
        //	Event Event = _unitOfWork.EventRepo.GetEventWithAllTranslations(id);
        //	return View(Event);
        //}


        public IActionResult Show(int id)
        {
            if (id == 13) // منع الوصول للـ 13 حتى لو اتكتب يدوي في الرابط
                return NotFound(); // أو RedirectToAction("Index")

            var ev = _unitOfWork.EventRepo.GetEventWithAllTranslations(id);
            if (ev == null)
                return NotFound(); // برضه لو مش موجود فعليًا

            return View(ev);
        }


        public IActionResult Upsert(int? id)
		{
			EventVM EventVM = new EventVM();
			//EventVM.Categories = _unitOfWork.CategoryRepo.GetAllCategoreisWithSpecificCategoryTranslation(_unitOfWork._defaultCulture);

			if (id == null || id == 0)
			{
				// create
				EventVM.Event = new Event();
				EventVM.EventTranslation = new EventTranslation();
			}
			else
			{
				// update
				EventVM.Event = _unitOfWork.EventRepo.GetEventInSpecificTranslation((int)id, _unitOfWork._defaultCulture);
				EventVM.EventTranslation = EventVM.Event.EventTranslations.FirstOrDefault();
			}

			return View(EventVM);

		}
		[HttpPost]
		public IActionResult Upsert(EventVM EventVM, List<IFormFile>? files)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// create
					if (EventVM.Event.Id.ToString() == null || EventVM.Event.Id == 0)
					{
						_unitOfWork.EventRepo.Add(EventVM.Event);
						_unitOfWork.Save();

						EventVM.EventTranslation.EventId = EventVM.Event.Id;
						_unitOfWork.EventTranslationRepo.Add(EventVM.EventTranslation);
						_unitOfWork.Save();
						TempData["success"] = "Event added successfully!";

						// Save Images
						if (files != null && files.Any())
						{
							AddEventImages(files, EventVM.Event.Id);
						}
					}
					// Update
					else
					{
						// Check if there are new images coming
						if (files != null && files.Any())
						{
							DeleteEventImages(EventVM.Event.Id);
							// Add new images
							AddEventImages(files, EventVM.Event.Id);

						}
						_unitOfWork.EventRepo.Update(EventVM.Event);
						_unitOfWork.EventTranslationRepo.Update(EventVM.EventTranslation);
						_unitOfWork.Save();
						TempData["success"] = "Event updated successfully!";
					}
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

			}
			TempData["error"] = "Error happened, please try again.";
			return RedirectToAction("Upsert", EventVM);

		}

		public IActionResult Delete(int id)
		{
			if (id.ToString() == null || id == 0)
			{
				return NotFound();
			}
			Event EventToBeDeleted = _unitOfWork.EventRepo.GetEventInSpecificTranslation(id, _unitOfWork._defaultCulture);
			return View(EventToBeDeleted);
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
			DeleteEventImages(id);

			// delete Event from db
			var EventToBeDeleted = _unitOfWork.EventRepo.Get(p => p.Id == id);
			if (EventToBeDeleted != null)
			{
				_unitOfWork.EventRepo.Remove(EventToBeDeleted);
				_unitOfWork.Save();
			}


			return RedirectToAction("Index", "Event", new { area = "Admin" });
		}



		private void AddEventImages(List<IFormFile> files, int id)
		{
			// save images to folder Events

			List<EventImage> ImagesList = new List<EventImage>();
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			string fileName = "";
			string saveFolderPath = Path.Combine(wwwRootPath, @"images\Events");
			foreach (var file in files)
			{
				fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				using (var fileStream = new FileStream
					(Path.Combine(saveFolderPath, fileName), FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				ImagesList.Add(new EventImage
				{
					EventId = id,
					ImageURL = Path.Combine(@"images\Events", fileName)
				});

			}
			// Save images to the database
			_unitOfWork.EventImageRepo.AddRange(ImagesList);
			_unitOfWork.Save();

		}

		private void DeleteEventImages(int id)
		{
			// Delete from folder
			List<EventImage> imagesToBeDeleted = _unitOfWork.EventImageRepo
				.Where(x => x.EventId == id).ToList();
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
			_unitOfWork.EventImageRepo.RemoveRange(imagesToBeDeleted);
			_unitOfWork.Save();

		}


	}
}

