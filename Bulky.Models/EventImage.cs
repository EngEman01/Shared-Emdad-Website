using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models
{
    public class EventImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageURL { get; set; }

        // Relationships
        [Required]
        public int EventId { get; set; }
        [ValidateNever]
        public Event Event { get; set; }
    }
}
