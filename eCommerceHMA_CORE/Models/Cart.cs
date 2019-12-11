using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceHMA_CORE.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
