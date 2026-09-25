using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleHub.Models;

namespace StyleHub.DataAccess.Repository
{
    public class ProductTranslationRepository : Repository<ProductTranslation>
    {
        public ProductTranslationRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
