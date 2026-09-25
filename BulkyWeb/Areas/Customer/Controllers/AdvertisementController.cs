using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.DataAccess;
using StyleHub.Models;
using StyleHub.Utility;

namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area(SD.Customer_Area)]
    public class AdvertisementController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public AdvertisementController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Advertisement> ads = _unitOfWork.AdRepo.GetAdsWithSpecificTranslation(getSelectedCulture());
            ads = ads.OrderByDescending(e => e.StartDateTime);
            return View(ads);
        }
        public IActionResult Show(int id)
        {
            var Advertisement = _unitOfWork.AdRepo.GetAdInSpecificTranslation(id, getSelectedCulture());
            return View(Advertisement);
        }
        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }
    }
}
