using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StyleHub.DataAccess;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Models.ViewModel;
using StyleHub.Utility;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace StyleHubWeb.Areas.Customer.Controllers
{
    [Area(SD.Customer_Area)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext context, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _configuration = configuration;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestQuote([FromForm] QuoteRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = string.Join("<br/>", errors) });
            }

            try
            {
                string smtpServer = _configuration["EmailSettings:EmailHost"] ?? "eltamawiya-emdad.com";
                int smtpPort = int.TryParse(_configuration["EmailSettings:EmailPort"], out int port) ? port : 465;
                string userName = _configuration["EmailSettings:EmailAddress"] ?? "info@eltamawiya-emdad.com";
                string password = _configuration["EmailSettings:EmailPassword"] ?? "Nb@*K#CNT!W{";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Emdad Website - Quote Request", userName));
                message.To.Add(new MailboxAddress("Emdad Info", "info@eltamawiya-emdad.com"));

                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    try
                    {
                        message.ReplyTo.Add(new MailboxAddress(model.FullName, model.Email));
                    }
                    catch { }
                }

                message.Subject = $"طلب عرض سعر: {model.ProductName} - ({model.FullName})";

                var builder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <div dir='rtl' style='font-family: Arial, Tahoma, sans-serif; background-color: #f7f9fa; padding: 30px; color: #333;'>
                        <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.08); border-top: 5px solid #d4af37;'>
                            <div style='background-color: #0b111e; padding: 25px; text-align: center;'>
                                <h2 style='color: #d4af37; margin: 0; font-size: 22px;'>شركة إمداد للصناعات</h2>
                                <p style='color: #e2e8f0; margin: 8px 0 0; font-size: 14px;'>طلب عرض سعر جديد للمنتج</p>
                            </div>
                            <div style='padding: 30px;'>
                                <table style='width: 100%; border-collapse: collapse;'>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; width: 35%; color: #0b111e;'>اسم المنتج:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #d4af37; font-weight: bold; font-size: 16px;'>{System.Net.WebUtility.HtmlEncode(model.ProductName)}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>كود المنتج (ID):</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'>#{model.ProductId}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>اسم العميل:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'>{System.Net.WebUtility.HtmlEncode(model.FullName)}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>البريد الإلكتروني:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'><a href='mailto:{System.Net.WebUtility.HtmlEncode(model.Email)}' style='color: #0d6efd;'>{System.Net.WebUtility.HtmlEncode(model.Email)}</a></td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>رقم الهاتف:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'><a href='tel:{System.Net.WebUtility.HtmlEncode(model.PhoneNumber)}' style='color: #0d6efd;'>{System.Net.WebUtility.HtmlEncode(model.PhoneNumber)}</a></td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>الجهة / الشركة:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'>{(string.IsNullOrWhiteSpace(model.Company) ? "غير محدد" : System.Net.WebUtility.HtmlEncode(model.Company))}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #0b111e;'>الكمية المطلوبة:</td>
                                        <td style='padding: 12px 10px; border-bottom: 1px solid #eee;'>{(string.IsNullOrWhiteSpace(model.Quantity) ? "غير محدد" : System.Net.WebUtility.HtmlEncode(model.Quantity))}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 12px 10px; vertical-align: top; font-weight: bold; color: #0b111e;'>ملاحظات / استفسار:</td>
                                        <td style='padding: 12px 10px; background-color: #fcfcfc; border-radius: 6px;'>{(string.IsNullOrWhiteSpace(model.Notes) ? "لا توجد ملاحظات إضافية" : System.Net.WebUtility.HtmlEncode(model.Notes).Replace("\n", "<br/>"))}</td>
                                    </tr>
                                </table>
                                <div style='margin-top: 25px; padding-top: 15px; border-top: 1px solid #e2e8f0; font-size: 12px; color: #888; text-align: center;'>
                                    تم إرسال هذا الطلب آلياً عبر موقع شركة إمداد بتاريخ {DateTime.Now:yyyy-MM-dd HH:mm}
                                </div>
                            </div>
                        </div>
                    </div>"
                };

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(userName, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Json(new { success = true, message = "تم إرسال طلب عرض السعر بنجاح، وسيتواصل معكم فريقنا في أقرب وقت." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء إرسال الطلب، يرجى المحاولة مرة أخرى لاحقاً: " + ex.Message });
            }
        }

        private string getSelectedCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var selectedCulture = rqf.RequestCulture.Culture.ToString();
            return selectedCulture;
        }
    }
}

