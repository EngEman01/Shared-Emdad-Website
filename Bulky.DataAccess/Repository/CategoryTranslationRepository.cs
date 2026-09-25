using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleHub.Models;

namespace StyleHub.DataAccess.Repository
{
    public class CategoryTranslationRepository : Repository<CategoryTranslation>
    {
        public CategoryTranslationRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
