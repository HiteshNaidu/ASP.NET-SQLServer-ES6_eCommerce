using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceHMA_CORE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double ShippingCost { get; set; }
        public string ImageURL { get; set; }
    }
}
