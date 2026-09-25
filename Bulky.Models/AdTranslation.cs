using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class AdTranslation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Language is required.")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }



        // Relationships
        [Required(ErrorMessage = "Advertisement ID is required.")]
        public int AdvertisementId { get; set; }
        [ValidateNever]
        public Advertisement Advertisement { get; set; }
    }
}
