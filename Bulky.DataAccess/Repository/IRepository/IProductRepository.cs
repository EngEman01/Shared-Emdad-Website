using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StyleHub.Models;

namespace StyleHub.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllProductsWithSpecificTranslation(string culture);
        public Product GetProductInSpecificTranslation(int id, string culture);
        public Product GetProductWithAllTranslations(int id);
        IEnumerable<Product> GetProductsOfCategoryWithSpecificTranslation(string culture, int categoryId);
        IEnumerable<dynamic> Get(Expression<Func<Product, bool>>? filter, string culture, string includeProperties);
        IEnumerable<Product> SearchProducts(string text, string selectedCulture);

    }
}
