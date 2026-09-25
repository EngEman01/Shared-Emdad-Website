using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StyleHub.Models
{
    public class ProductTranslation
    {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please, enter the Product Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter the Product Description.")]
        public string Description { get; set; } = string.Empty;

        // Relationships
        [Required]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
    }
}
