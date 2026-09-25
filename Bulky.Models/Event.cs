using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // Relationships
        // ProductTranslations
        [ValidateNever]
        public List<EventTranslation> EventTranslations { get; set; }

        // Images
        [ValidateNever]
        [DisplayName("Event Images")]
        public List<EventImage>? Images { get; set; }
    }
}
