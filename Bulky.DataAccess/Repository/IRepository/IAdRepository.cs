using StyleHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.DataAccess.Repository.IRepository
{
    public interface IAdRepository : IRepository<Advertisement>
    {
        IEnumerable<Advertisement> GetAllAdsWithSpecificTranslation(string culture);
        Advertisement GetAdInSpecificTranslation(int id, string culture);
        IEnumerable<Advertisement> GetAdsWithSpecificTranslation(string selectedCulture);
        Advertisement GetAdWithAllTranslations(int id);
        IEnumerable<dynamic> Get(Expression<Func<Advertisement, bool>>? filter, string culture, string includeProperties);
        IEnumerable<Advertisement> SearchAds(string text, string selectedCulture);
    }
}
