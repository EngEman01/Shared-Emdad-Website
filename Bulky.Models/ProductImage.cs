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
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        //[ImageUrl(ErrorMessage ="Please, enter a valid image URL. Image URL must end with .jpg or .png.")] 
        public string ImageURL { get; set; }

        // Relationships
        [Required]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
    }
}
