using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StyleHub.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date and time is required.")]
        [Display(Name = "Start Date and Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Start date must be a valid date.")]
        //[StartDate]
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        
        [Display(Name = "End Date and Time")]
        [DataType(DataType.DateTime, ErrorMessage = "End date must be a valid date.")]
        //[EndDate(DateStartProperty = "StartDateTime")]
        public DateTime? EndDateTime { get; set; }

        
        // Relationships
        // Images
        [ValidateNever]
        public List<AdImage> Images { get; set; }


        // Translation
        [ValidateNever]
        public List<AdTranslation> Translations { get; set; }



    }
}
