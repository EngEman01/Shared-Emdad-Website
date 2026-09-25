using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace StyleHub.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IRepositoryCategory CategoryRepo { get; }
        public IProductRepository ProductRepo { get; }
        public IAdRepository AdRepo { get; }
        public IEventRepository EventRepo { get; }

        public ProductImageRepository ProductImageRepo { get; }
        public EventImageRepository EventImageRepo { get; }
        public AdImageRepository AdImageRepo { get; }

        public CategoryTranslationRepository CategoryTranslationRepo { get; }
        public ProductTranslationRepository ProductTranslationRepo { get; }
        public AdTranslationRepository AdTranslationRepo { get; }
        public EventTranslationRepository EventTranslationRepo { get; }

        public IOptions<RequestLocalizationOptions> LocalizationOptions { get; }
        public string _defaultCulture { get; }
        public void Save();
    }
}
