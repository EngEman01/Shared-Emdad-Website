using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace StyleHub.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        // Relationships

        // Category
        [Required]
        public int CategoryId { get; set; }     // Foreign Key
        [ValidateNever]
        public Category Category { get; set; }  // Navigation Property

        // ProductTranslations
        [ValidateNever]
        public List<ProductTranslation> ProductTranslations { get; set; }

        // Images
        [ValidateNever]
        [DisplayName("Product Images")]
        public List<ProductImage>? Images { get; set; }



    }
}
