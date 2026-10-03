using System.ComponentModel.DataAnnotations;

namespace StyleHub.Models.ViewModel
{
    public class QuoteRequestVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "يرجى إدخال الاسم / Please enter your name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "يرجى إدخال البريد الإلكتروني / Please enter your email")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة / Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "يرجى إدخال رقم الهاتف / Please enter your phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Company { get; set; }

        public string? Quantity { get; set; }

        public string? Notes { get; set; }
    }
}
