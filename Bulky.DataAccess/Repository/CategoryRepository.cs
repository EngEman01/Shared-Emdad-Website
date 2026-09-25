using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleHub.Models;
using StyleHub.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StyleHub.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, IRepositoryCategory
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // get all categoreis with their navigation propertity category translations
        public IEnumerable<Category> GetAllCategoreisWithAllCategoryTranslations()
        {
            var result = _db.Categories.Include("CategoryTranslations").Include(c => c.ParentCategory.CategoryTranslations);

            return result;
        }
        public IEnumerable<Category> GetAllCategoreisWithSpecificCategoryTranslation(string culture)
        {
            var result = _db.Categories
            .Include(c => c.CategoryTranslations)
            .Include(c => c.ParentCategory.CategoryTranslations)
            .Select(c => new Category
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategory = c.ParentCategory,
                CategoryTranslations = c.CategoryTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            });

            return result;
        }
        public IEnumerable<Category> GetAllSubCategoreisWithSpecificCategoryTranslation(string culture)
        {
            var result = _db.Categories
            .Where(c => c.ParentCategoryId != null)
            .Include(c => c.CategoryTranslations)
            .Include(c => c.ParentCategory.CategoryTranslations)
            .Select(c => new Category
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategory = c.ParentCategory,
                CategoryTranslations = c.CategoryTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            });

            return result;
        }
        public IEnumerable<Category> GetAllParentCategoriesWithSpecificCategoryTranslation(string culture)
        {
            var result = _db.Categories
            .Where(c => c.ParentCategoryId == null)
            .Include(c => c.CategoryTranslations)
            .Select(c => new Category
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategory = c.ParentCategory,
                CategoryTranslations = c.CategoryTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            });

            return result;
        }

        public Category GetCategoryWithAllCategoryTranslation(int categoryId)
        {
            var category = _db.Categories.Where(c => c.Id == categoryId).Include(c => c.CategoryTranslations)
                .Include(c => c.ParentCategory).FirstOrDefault();
            //    .Select(c => new Category
            //{
            //    Id = c.Id,
            //    ImageUrl = c.ImageUrl,
            //    ParentCategoryId = c.ParentCategoryId,
            //    ParentCategory = c.ParentCategory,
            //    CategoryTranslations = c.CategoryTranslations.ToList()
            //}).FirstOrDefault();

            return category;
        }

        public Category GetCategoryWithSpecificCategoryTranslation(int categoryId, string culture)
        {
            var category = _db.Categories.Where(c => c.Id == categoryId).Include(c => c.CategoryTranslations).Select(c => new Category
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategory = c.ParentCategory,
                CategoryTranslations = c.CategoryTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            }).FirstOrDefault();

            return category;
        }

        public IEnumerable<Category> GetSubCategoreisWithSpecificCategoryTranslation(int categoryId, string culture)
        {
            var result = _db.Categories
           .Where(c => c.ParentCategoryId == categoryId)
           .Include(c => c.CategoryTranslations)
           .Include(c => c.ParentCategory.CategoryTranslations)
           .Select(c => new Category
           {
               Id = c.Id,
               ImageUrl = c.ImageUrl,
               ParentCategoryId = c.ParentCategoryId,
               ParentCategory = c.ParentCategory,
               CategoryTranslations = c.CategoryTranslations.Where(c => c.Language.ToLower() == culture).ToList()
           });

            return result;
        }
    }
}
