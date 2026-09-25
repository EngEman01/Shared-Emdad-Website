using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using StyleHub.Models;

namespace StyleHub.Utility
{
    public static class TestDataGenerator
    {
        private static int _categoryId = 1;
        private static int _categoryTranslationId = 1;
        private static int _productId = 1;
        private static int _productTranslationId = 1;
        private static int _productImageId = 1;

        public static List<Category> GenerateCategories(int count = 15)
        {
            var faker = new Faker<Category>()
                .RuleFor(c => c.Id, _ => _categoryId++)
                //.RuleFor(c => c.DefaultName, f => f.Commerce.Categories(1)[0])
                //.RuleFor(c => c.DefaultDescription, f => f.Lorem.Paragraph())
                .RuleFor(c => c.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(c => c.ParentCategoryId, f => f.Random.Bool(0.2f) ? f.Random.Int(1, count) : (int?)null) // 20% chance to have a parent
                .RuleFor(c => c.SubCategories, _ => new List<Category>())
                .RuleFor(c => c.CategoryTranslations, _ => new List<CategoryTranslation>());

            var categories = faker.Generate(count);

            // Link subcategories and translations
            foreach (var category in categories)
            {
                if (category.ParentCategoryId.HasValue)
                {
                    category.ParentCategory = categories.Find(c => c.Id == category.ParentCategoryId);
                    category.ParentCategory?.SubCategories.Add(category);
                }

                category.CategoryTranslations = GenerateCategoryTranslations(category.Id, 2);
            }

            return categories;
        }

        public static List<CategoryTranslation> GenerateCategoryTranslations(int categoryId, int count)
        {
            var faker = new Faker<CategoryTranslation>()
                .RuleFor(ct => ct.Id, _ => _categoryTranslationId++) // Ensure unique IDs
                .RuleFor(ct => ct.Language, f => f.PickRandom(new[] { "en-us", "es" }))
                .RuleFor(ct => ct.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(ct => ct.Description, f => f.Lorem.Sentence())
                .RuleFor(ct => ct.CategoryId, _ => categoryId);

            return faker.Generate(count);
        }

        public static List<Product> GenerateProducts(int count, List<Category> categories)
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.Id, _ => _productId++)
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(p => p.ProductTranslations, _ => new List<ProductTranslation>())
                .RuleFor(p => p.Images, _ => new List<ProductImage>());

            var products = faker.Generate(count);

            foreach (var product in products)
            {
                product.ProductTranslations = GenerateProductTranslations(product.Id, 2);
                product.Images = GenerateProductImages(product.Id, 3);
            }

            return products;
        }

        public static List<ProductTranslation> GenerateProductTranslations(int productId, int count)
        {
            var faker = new Faker<ProductTranslation>()
                .RuleFor(pt => pt.Id, _ => _productTranslationId++)
                .RuleFor(pt => pt.Language, f => f.PickRandom(new[] { "en-us", "es" }))
                .RuleFor(pt => pt.Name, f => f.Commerce.ProductName())
                .RuleFor(pt => pt.Description, f => f.Commerce.ProductDescription())
                .RuleFor(pt => pt.ProductId, _ => productId);

            return faker.Generate(count);
        }

        public static List<ProductImage> GenerateProductImages(int productId, int count)
        {
            var faker = new Faker<ProductImage>()
                .RuleFor(pi => pi.Id, _ => _productImageId++)
                .RuleFor(pi => pi.ImageURL, f => f.Image.PicsumUrl())
                .RuleFor(pi => pi.ProductId, _ => productId);

            return faker.Generate(count);
        }
    }
}
