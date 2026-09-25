using Microsoft.Identity.Client;
using StyleHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.DataAccess.Repository
{
    public class EventImageRepository : Repository<EventImage>
    {
        public EventImageRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
