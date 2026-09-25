using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StyleHub.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class AdImage
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Image URL is Required.")]
        //[ImageUrl]
        public string ImageURL { get; set; }

        // Relationships
        [Required(ErrorMessage = "Advertisemnet ID is required")]
        public int AdvertisementId { get; set; }
        [ValidateNever]
        public Advertisement Advertisement { get; set; }
    }
}
