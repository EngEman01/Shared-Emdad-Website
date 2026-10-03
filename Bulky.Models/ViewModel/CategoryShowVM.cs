using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models.ViewModel
{
    public class CategoryShowVM
    {
        public Category Category { get; set; }
        public IEnumerable<CategoryTranslation> CategoryTranslations { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }
    }
}
