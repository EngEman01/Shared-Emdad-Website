using StyleHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.DataAccess.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetAllEventsWithSpecificTranslation(string culture);
        public Event GetEventInSpecificTranslation(int id, string culture);
        public Event GetEventWithAllTranslations(int id);
        IEnumerable<Event> GetEventsWithSpecificTranslation(string selectedCulture);
        IEnumerable<dynamic> Get(Expression<Func<Event, bool>>? filter, string culture, string includeProperties);
        IEnumerable<Event> SearchEvents(string text, string selectedCulture);
    }
}
