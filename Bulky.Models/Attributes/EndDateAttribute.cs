using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;


namespace StyleHub.Models.Attributes
{
    public class EndDateAttribute : ValidationAttribute
    {
        public string DateStartProperty { get; set; }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //Get Value of the DateStart property
        //    string dateStartString = HttpContext.Current.Request[DateStartProperty];
        //    DateTime dateEnd = (DateTime)value;
        //    DateTime dateStart = DateTime.Parse(dateStartString);


        //    if (dateStart < dateEnd)
        //    {
        //        return ValidationResult.Success;
        //    }
        //    return new ValidationResult("The start date and time must be before the end time and time");
        //}

    }
}
