using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StyleHub.Models
{
    public class CategoryTranslation
    {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Category Name Translation is required.")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;



        // Relationships
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
