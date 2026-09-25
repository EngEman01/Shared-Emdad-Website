using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models.Attributes
{
    public class StartDateAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           DateTime dateStart = (DateTime) value;
            if(dateStart >= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The start date and time must be in the future");  

        }
    }
}
