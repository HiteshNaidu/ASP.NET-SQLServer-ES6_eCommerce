using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceHMA_CORE.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public decimal rating { get; set; }
        public string ImageURL { get; set; }
        public string Text { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
