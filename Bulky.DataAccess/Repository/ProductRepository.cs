using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Localization;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using StyleHub.Models.ViewModel;
using System.Text.RegularExpressions;


namespace StyleHub.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ProductImage ProductImage { get; set; }
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            //this._defaultCulture = localizationOptions.Value.DefaultRequestCulture.Culture.Name;
        }

        public IEnumerable<Product> GetAllProductsWithSpecificTranslation(string culture)
        {
            IEnumerable<Product> products = _db.Products
                .Include(p => p.ProductTranslations.Where(t => t.Language.ToLower() == culture.ToLower()))
                .Include(p => p.Images)
                .Include(p => p.Category.CategoryTranslations);

            return products;
        }
        public Product GetProductInSpecificTranslation(int id, string culture)
        {
            var product = _db.Products.Where(p => p.Id == id).Include(p => p.ProductTranslations).Include(p => p.Images).Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Images = p.Images,
                ProductTranslations = p.ProductTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            }).FirstOrDefault();

            return product;
        }
        public Product GetProductWithAllTranslations(int id)
        {
            var product = _db.Products.Where(p => p.Id == id).Include(p => p.ProductTranslations).Include(p => p.Images).Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Images = p.Images,
                ProductTranslations = p.ProductTranslations.ToList()
            }).FirstOrDefault();

            return product;
        }
        public IEnumerable<Product> GetProductsOfCategoryWithSpecificTranslation(string selectedCulture, int categoryId)
        {
            IEnumerable<Product> productsOfCategory = _db.Products.Where(p => p.CategoryId == categoryId)
                .Include(p => p.ProductTranslations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Include(p => p.Images)
                .Include(p => p.Category);

            return productsOfCategory;
        }

        public IEnumerable<dynamic> Get(Expression<Func<Product, bool>>? filter, string culture, string includeProperties)
        {
            IQueryable<Product> query = _db.Products;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }
            var products = query.Select(p => new
            {
                p.Id,
                p.CategoryId,
                // Use _defaultCulture for translations
                TranslatedName = p.ProductTranslations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Name)
                        .FirstOrDefault() ?? "None",
                TranslatedDescription = p.ProductTranslations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Description)
                        .FirstOrDefault() ?? "None",
              Category = p.Category,

            })
                .ToList();

            
            return products;
        }

		IEnumerable<Product> IProductRepository.SearchProducts(string text, string selectedCulture)
		{
            IEnumerable<Product> prodcuts = _db.Products
                .Include(p => p.Images)
                .Include(p => p.ProductTranslations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Where(p => p.ProductTranslations.Any(t => t.Name.Contains(text) || t.Description.Contains(text)));
            return prodcuts;
		}
	}
}
