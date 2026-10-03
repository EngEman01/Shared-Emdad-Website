using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StyleHub.Models.ViewModel
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public CategoryTranslation CategoryTranslation { get; set; }
    }
}
