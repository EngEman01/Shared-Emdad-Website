using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleHub.Models;

namespace StyleHub.DataAccess.Repository
{
    public class AdTranslationRepository : Repository<AdTranslation>
    {
        public AdTranslationRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
