using StyleHub.Models;


namespace StyleHub.DataAccess.Repository.IRepository
{
    public interface IRepositoryCategory : IRepository<Category>
    {
        IEnumerable<Category> GetAllCategoreisWithAllCategoryTranslations();
        IEnumerable<Category> GetAllCategoreisWithSpecificCategoryTranslation(string culture);
        IEnumerable<Category> GetAllParentCategoriesWithSpecificCategoryTranslation(string culture);
        Category GetCategoryWithSpecificCategoryTranslation(int categoryId, string culture);
        Category GetCategoryWithAllCategoryTranslation(int categoryId);
        IEnumerable<Category> GetAllSubCategoreisWithSpecificCategoryTranslation(string culture);
        IEnumerable<Category> GetSubCategoreisWithSpecificCategoryTranslation(int categoryId,string culture);

    }
}

