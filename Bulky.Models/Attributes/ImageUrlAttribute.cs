using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace StyleHub.Models.Attributes
{
    public class ImageUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value.ToString();

            if (!string.IsNullOrEmpty(url)
                &&(url.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                || url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                || url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Image URL must end with .png or .jpg.");
        }
    }
}
