using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.DataAccess;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area(SD.Customer_Area)]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public EventController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            //IEnumerable<Event> Events = _unitOfWork.EventRepo.GetEventsWithSpecificTranslation(getSelectedCulture());
            //return View(Events);

            // Fetch events with specific translation
            IEnumerable<Event> events = _unitOfWork.EventRepo.GetEventsWithSpecificTranslation(getSelectedCulture());

            // Sort events by date from newest to oldest
            events = events.OrderByDescending(e => e.Date);

            return View(events);
        }
        public IActionResult Show(int id)
        {
            var Event = _unitOfWork.EventRepo.GetEventInSpecificTranslation(id, getSelectedCulture());
            return View(Event);
        }
        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }
    }
}
