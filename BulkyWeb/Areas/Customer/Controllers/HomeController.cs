using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Models.ViewModel;
using StyleHub.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using MimeKit;
using MailKit.Security;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MailKit.Net.Smtp;


namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IHtmlLocalizer _localizer;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            //_localizer = localizer;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> mainCategories = _unitOfWork.CategoryRepo.GetAllParentCategoriesWithSpecificCategoryTranslation(getSelectedCulture());
            ViewBag.MainCategories = mainCategories;

            ViewBag.Title = "الصفحة الرئيسية";
            ViewBag.MetaDescription = "امداد متخصصون في الصناعات والمنتجات العسكرية والمعدات والخامات العسكرية، وكذلك في المواد البترولية واحتياجات ومستلزمات الشركات البترولية والمشروعات الخاصة بها. نقدم حلول متكاملة وخدمات لوجستية عالية الجودة.";
            ViewBag.MetaKeywords = "أسلحة, عسكري, دفاعي, دبابات, مدرعات, طائرات, صواريخ, بنادق, ذخائر, معدات عسكرية, معدات بترولية, بترول, خدمات لوجستية, امداد, إمداد, الصناعات العسكرية, المواد البترولية, المعدات الدفاعية, تصنيع عسكري, توريدات عسكرية, حلول صناعية, شركات البترول, مشاريع صناعية, تكنولوجيا الدفاع, معدات السلامة المهنية, مكافحة الحرائق, وحدات معالجة المياه, أنظمة الذكاء الاصطناعي العسكري, الطباعة ثلاثية الأبعاد العسكرية, أنظمة صناعية ذكية, تطوير الصناعات الوطنية, البحث والتطوير الصناعي, شراكات حكومية, شراكات دولية, تصنيع مصري, مشاريع دفاعية, معدات السلامة الصناعية, منتجات دفاعية, معدات الطوارئ, سيارات صهريجية, طرمبات بنزين, ولاعات أفران صناعية";


            // Prepare HomeVM with events
            var vm = new HomeVM();
            try
            {
                var events = _unitOfWork.EventRepo.GetEventsWithSpecificTranslation(getSelectedCulture());
                vm.Events = events;
            }
            catch
            {
                vm.Events = Enumerable.Empty<Event>();
            }

            return View(vm);
        }

        public IActionResult AboutUs()
        {
            ViewBag.Title = "عن شركة إمداد";
            ViewBag.MetaDescription = "تعرف على شركة إمداد، تاريخها، رؤيتها، وأهدافها في الصناعات الدفاعية والبترولية.";
            ViewBag.MetaKeywords = "إمداد, عن الشركة, تاريخ الشركة, أهداف الشركة, رؤية الشركة, قيم الشركة, الصناعات العسكرية, الصناعات الدفاعية, المواد البترولية, المعدات الدفاعية, المنتجات الدفاعية, الخدمات اللوجستية, شراكات حكومية, شراكات دولية, تطوير الصناعات الوطنية, الابتكار الصناعي, البحث والتطوير, تكنولوجيا الدفاع, حلول صناعية متكاملة, مشاريع بترولية, مشاريع صناعية, تصنيع مصري, مشاريع دفاعية, معدات السلامة المهنية, وحدات معالجة المياه, مكافحة الحرائق, التوريدات الصناعية, الذكاء الاصطناعي في التصنيع";
            return View();
        }

        public IActionResult CompanyStructure()
        {
            ViewBag.Title = "هيكل شركة إمداد";
            ViewBag.MetaDescription = "تعرف على شركة إمداد، تاريخها، رؤيتها، وأهدافها في الصناعات الدفاعية والبترولية.";
            ViewBag.MetaKeywords = "إمداد, عن الشركة, تاريخ الشركة, أهداف الشركة, رؤية الشركة, قيم الشركة, الصناعات العسكرية, الصناعات الدفاعية, المواد البترولية, المعدات الدفاعية, المنتجات الدفاعية, الخدمات اللوجستية, شراكات حكومية, شراكات دولية, تطوير الصناعات الوطنية, الابتكار الصناعي, البحث والتطوير, تكنولوجيا الدفاع, حلول صناعية متكاملة, مشاريع بترولية, مشاريع صناعية, تصنيع مصري, مشاريع دفاعية, معدات السلامة المهنية, وحدات معالجة المياه, مكافحة الحرائق, التوريدات الصناعية, الذكاء الاصطناعي في التصنيع";
            return View();
        }
        public IActionResult NewsRoom()
        {
            ViewBag.Title = "غرفة الأخبار - شركة إمداد";
            ViewBag.MetaDescription = "تابع أحدث الأخبار، الفعاليات، الأنشطة، والإعلانات الخاصة بشركة إمداد في المجالات الدفاعية والبترولية، بما في ذلك الصناعات العسكرية والمعدات الدفاعية والمشروعات البترولية.";
            ViewBag.MetaKeywords = "إمداد, أخبار الشركة, الأخبار الصناعية, الأخبار الدفاعية, الأخبار البترولية, الفعاليات, الأحداث, الإعلانات, الصناعات العسكرية, الصناعات الدفاعية, المشاريع البترولية, التحديثات الصناعية, الأخبار المحلية, الأخبار الدولية, شراكات الشركة, الأنشطة الصحفية, المنشورات الرسمية, مقالات صحفية, أخبار الدفاع, أخبار البترول, الأخبار التكنولوجية, الابتكار الصناعي, الذكاء الاصطناعي في الصناعات الدفاعية, المعدات العسكرية, المعدات البترولية, توريدات عسكرية, توريدات بترولية, حلول صناعية متكاملة, مشاريع حكومية, شراكات دولية, تطوير الصناعات, تكنولوجيا التصنيع العسكري, السلامة المهنية, معالجة المياه, مكافحة الحرائق, المعدات الثقيلة, المعدات اللوجستية";
            return View();
        }
        public IActionResult Details(int id)
        {
            Product productFromDb = _unitOfWork.ProductRepo.Get(p => p.Id == id, includeProperties: "Category,Images");

            return View(productFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Products(int? Id)
        {
            List<Product> products = new List<Product>();
            if (Id == null)
            {
                products = _unitOfWork.ProductRepo.GetAll(includeProperties: "Images").ToList();
                products = Helper.ShuffleList<Product>(products);
            }
            else
            {
                products = _unitOfWork.ProductRepo.Where(p => p.CategoryId == Id, includeProperties: "Images").ToList();
            }

            return View(products);
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            ViewBag.Title = "تواصل معنا - شركة إمداد";
            ViewBag.MetaDescription = "اتصل بشركة إمداد للحصول على معلومات عن منتجاتها، خدماتها، الدعم الفني، أو أي استفسارات تتعلق بالصناعات الدفاعية والبترولية والمعدات واللوجستيات الصناعية.";
            ViewBag.MetaKeywords = "إمداد, اتصل بنا, تواصل مع الشركة, استفسار, دعم فني, خدمات العملاء, الصناعات الدفاعية, الصناعات العسكرية, المواد البترولية, المعدات الدفاعية, الخدمات اللوجستية, البريد الإلكتروني, أرقام الهواتف, العنوان, التواصل مع إدارة الشركة, شكاوى العملاء, استفسارات المشاريع, توريدات صناعية, مشاريع بترولية, حلول صناعية متكاملة, استشارات صناعية, عقود حكومية, تطوير المشروعات, معدات السلامة المهنية, معدات مكافحة الحرائق, معالجة مياه, وحدات معالجة الصرف, معدات النفط, توريد للمؤسسات, معدات الإنتاج الصناعي, شراكات مع شركات دولية, تقديم عروض الأسعار, طلب منتجات صناعية, متابعة الطلبات, دعم الشركات البترولية, حلول لوجستية متكاملة, إدارة المشاريع الصناعية";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // SMTP Configuration
                    string smtpServer = "eltamawiya-emdad.com";
                    int smtpPort = 465; // SSL Port
                    string userName = "info@eltamawiya-emdad.com";
                    string password = "Nb@*K#CNT!W{";

                    // Create the Email Message
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Emdad Website Contact Form", "info@eltamawiya-emdad.com"));
                    message.To.Add(new MailboxAddress("Support", "info@eltamawiya-emdad.com"));
                    message.Subject = "New Contact Form Submission";

                    // Email Body
                    var builder = new BodyBuilder
                    {
                        TextBody = $@"
                        Name: {model.FirstName} {model.LastName}
                        Email: {model.Email}
                        Phone: {model.Phone}
                        Entity: {model.Entity}
                        Message: {model.Message}"
                    };
                    message.Body = builder.ToMessageBody();

                    // Configure the email client
                    using (var client = new SmtpClient())
                    {
                        // Use SSL/TLS directly for port 465
                        await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.SslOnConnect);
                        await client.AuthenticateAsync(userName, password);

                        // Send the email
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    return RedirectToAction("Confirmation");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error sending message: " + ex.Message);
                    Console.WriteLine($"Error: {ex}");
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        // البطاقة الضريبية
        public IActionResult TaxCard()
        {
            return View();
        }
        // الشركاء
        public IActionResult Partners()
        {
            return View();

        }
        public IActionResult BoardOfDirectors()
        {
            return View();

        }
        public IActionResult Goals()
        {
            return View();

        }
        public IActionResult CommercialRegister()
        {
            return View();

        }
        public IActionResult SearchProducts(string text)
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepo.SearchProducts(text, getSelectedCulture());
            return View(products);
        }

        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }
    }
}
