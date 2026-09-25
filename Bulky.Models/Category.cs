using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using StyleHub.Models.Attributes;

namespace StyleHub.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        //[DisplayName("Default Name")]
        ////[Required(ErrorMessage = "Category default name is required.")]
        //public string? DefaultName { get; set; } = "Category Default Name";
        //[Display(Name = "Default Description")]
        ////[Required(ErrorMessage = "Category default description is required.")]
        //public string? DefaultDescription { get; set; } = "Category Default Description";

        [DisplayName("Category Image")]
        //[Required]
        //[ImageUrl(ErrorMessage ="Please enter a valid URL. Image URL must end with .png or .jpg.")]
        public string? ImageUrl { get; set; }


        // Relationships

        // Products 
        [ValidateNever]
        public IEnumerable<Product> Products { get; set; }
        // Parent Categories
        public int? ParentCategoryId { get; set; }
        [ValidateNever]
        public Category? ParentCategory { get; set; }


        // SubCategories
        [ValidateNever]
        public List<Category> SubCategories { get; set; }

        // Translations
        [ValidateNever]
        public List<CategoryTranslation>? CategoryTranslations { get; set; }




    }
}
