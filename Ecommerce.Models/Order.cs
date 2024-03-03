using Ecommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime DateTime { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }


        public List<Cart> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<Cart>();
        }
        public override string ToString()
        {
            return $"cart created in {DateTime}";
        }
    }
}
