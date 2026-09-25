using StyleHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.DataAccess.Repository
{
    public class EventTranslationRepository : Repository<EventTranslation>
    {
        public EventTranslationRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
