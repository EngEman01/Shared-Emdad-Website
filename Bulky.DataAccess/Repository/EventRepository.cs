using Microsoft.EntityFrameworkCore;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.DataAccess.Repository
{
    public class EventRepository :Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _db;

        public EventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Event> GetAllEventsWithSpecificTranslation(string culture)
        {
            IEnumerable<Event> events = _db.Events
                .Include(p => p.EventTranslations.Where(t => t.Language.ToLower() == culture.ToLower()))
                .Include(p => p.Images);

            return events;
        }
        public Event GetEventInSpecificTranslation(int id, string culture)
        {
            var _event = _db.Events.Where(p => p.Id == id).Include(p => p.EventTranslations).Include(p => p.Images).Select(p => new Event
            {
                Id = p.Id,
                Images = p.Images,
                EventTranslations = p.EventTranslations.Where(c => c.Language.ToLower() == culture).ToList()
            }).FirstOrDefault();

            return _event;
        }
        public Event GetEventWithAllTranslations(int id)
        {
            var _event = _db.Events.Where(p => p.Id == id).Include(p => p.EventTranslations).Include(p => p.Images).Select(p => new Event
            {
                Id = p.Id,
                Images = p.Images,
                EventTranslations = p.EventTranslations.ToList()
            }).FirstOrDefault();

            return _event;
        }
        public IEnumerable<Event> GetEventsWithSpecificTranslation(string selectedCulture)
        {
            IEnumerable<Event> Events = _db.Events
                .Include(p => p.EventTranslations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Include(p => p.Images);

            return Events;
        }

        public IEnumerable<dynamic> Get(Expression<Func<Event, bool>>? filter, string culture, string includeProperties)
        {
            IQueryable<Event> query = _db.Events;
            if (filter != null)
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
            var Events = query.Select(p => new
            {
                p.Id,
                // Use _defaultCulture for translations
                TranslatedName = p.EventTranslations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Name)
                        .FirstOrDefault() ?? "None",
                TranslatedDescription = p.EventTranslations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Description)
                        .FirstOrDefault() ?? "None",

            })
                .ToList();


            return Events;
        }

        IEnumerable<Event> IEventRepository.SearchEvents(string text, string selectedCulture)
        {
            IEnumerable<Event> ads = _db.Events
                .Include(p => p.Images)
                .Include(p => p.EventTranslations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Where(p => p.EventTranslations.Any(t => t.Name.Contains(text) || t.Description.Contains(text)));
            return ads;
        }
    }
}
