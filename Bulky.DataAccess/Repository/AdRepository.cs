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
    public class AdRepository : Repository<Advertisement>, IAdRepository
    {
        private readonly ApplicationDbContext _db;

        public AdRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Advertisement> GetAllAdsWithSpecificTranslation(string culture)
        {
            IEnumerable<Advertisement> ads = _db.Advertisements
                .Include(p => p.Translations.Where(t => t.Language.ToLower() == culture.ToLower()))
                .Include(p => p.Images);

            return ads;
        }
        public Advertisement GetAdInSpecificTranslation(int id, string culture)
        {
            //var advertisement = _db.Advertisements.Where(p => p.Id == id).Include(p => p.Translations).Include(p => p.Images).FirstOrDefault();

            var advertisement = _db.Advertisements.Where(p => p.Id == id).Include(p => p.Translations).Include(p => p.Images).Select(p => new Advertisement
            {
                Id = p.Id,
                Images = p.Images,
                StartDateTime = p.StartDateTime,
                EndDateTime = p.EndDateTime ?? DateTime.MaxValue,
                Translations = p.Translations.Where(c => c.Language.ToLower() == culture).ToList()
            }).FirstOrDefault();

            return advertisement;
        }
        public Advertisement GetAdWithAllTranslations(int id)
        {
            var advertisement = _db.Advertisements.Where(p => p.Id == id).Include(p => p.Translations).Include(p => p.Images).Select(p => new Advertisement
            {
                Id = p.Id,
                Images = p.Images,
                Translations = p.Translations.ToList()
            }).FirstOrDefault();

            return advertisement;
        }
        public IEnumerable<Advertisement> GetAdsWithSpecificTranslation(string selectedCulture)
        {
            IEnumerable<Advertisement> ads = _db.Advertisements
                .Include(p => p.Translations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Include(p => p.Images);

            return ads;
        }

        public IEnumerable<dynamic> Get(Expression<Func<Advertisement, bool>>? filter, string culture, string includeProperties)
        {
            IQueryable<Advertisement> query = _db.Advertisements;
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
            var ads = query.Select(p => new
            {
                p.Id,
                // Use _defaultCulture for translations
                TranslatedName = p.Translations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Name)
                        .FirstOrDefault() ?? "None",
                TranslatedDescription = p.Translations
                        .Where(t => t.Language.ToLower() == culture.ToLower())
                        .Select(t => t.Description)
                        .FirstOrDefault() ?? "None",

            })
                .ToList();


            return ads;
        }

        IEnumerable<Advertisement> IAdRepository.SearchAds(string text, string selectedCulture)
        {
            IEnumerable<Advertisement> ads = _db.Advertisements
                .Include(p => p.Images)
                .Include(p => p.Translations.Where(t => t.Language.ToLower() == selectedCulture.ToLower()))
                .Where(p => p.Translations.Any(t => t.Name.Contains(text) || t.Description.Contains(text)));
            return ads;
        }

    }
}
