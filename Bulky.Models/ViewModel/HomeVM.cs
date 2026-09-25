using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleHub.Models.ViewModel
{
    public  class HomeVM
    {
        public IEnumerable<Product> womenProducts { get; set; }
        public IEnumerable<Product> menProducts { get; set; }
        public IEnumerable<Product> kidsProducts { get; set; }
        public IEnumerable<Product> accessoriesProducts { get; set; }

        // Add events list so Home view can render events while keeping HomeVM as model
        public IEnumerable<Event> Events { get; set; }

    }
}
