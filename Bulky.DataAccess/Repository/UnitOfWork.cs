using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleHub.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;


namespace StyleHub.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IRepositoryCategory CategoryRepo { get; private set; }
        public IProductRepository ProductRepo { get; private set; }
        public IEventRepository EventRepo { get; private set; }
        public IAdRepository AdRepo { get; private set; }

        public ProductImageRepository ProductImageRepo { get; private set; }
        public AdImageRepository AdImageRepo { get; private set; }
        public EventImageRepository EventImageRepo { get; private set; }

        public CategoryTranslationRepository CategoryTranslationRepo { get; private set; }
        public ProductTranslationRepository ProductTranslationRepo { get; private set; }
        public AdTranslationRepository AdTranslationRepo { get; private set; }
        public EventTranslationRepository EventTranslationRepo { get; private set; }

        public IOptions<RequestLocalizationOptions> LocalizationOptions { get; private set; }

        public string _defaultCulture { get; set; }

        public UnitOfWork(ApplicationDbContext db, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo = new ProductRepository(_db);
            AdRepo = new AdRepository(_db);
            EventRepo = new EventRepository(_db);
            
            ProductImageRepo = new ProductImageRepository(_db);
            AdImageRepo = new AdImageRepository(_db);
            EventImageRepo = new EventImageRepository(_db);

            CategoryTranslationRepo = new CategoryTranslationRepository(_db);
            ProductTranslationRepo = new ProductTranslationRepository(_db);
            AdTranslationRepo = new AdTranslationRepository(_db);
            EventTranslationRepo = new EventTranslationRepository(_db);

            LocalizationOptions = localizationOptions;
            _defaultCulture = LocalizationOptions.Value.DefaultRequestCulture.Culture.Name;

        }


        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
