using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class EventTranslation
    {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please, enter the Event Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter the Event Description.")]
        public string Description { get; set; } = string.Empty;

        // Relationships
        [Required]
        public int EventId { get; set; }
        [ValidateNever]
        public Event Event { get; set; }
    }
}
