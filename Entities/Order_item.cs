using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class Order_item
    {
        public int Id { get; set; }
        public ICollection<ProductInfo> ProductsInfo { get; set; }
        public Order Orders { get; set; }
        public string TargetDay { get; set; }
        public bool Completed { get; set; } = false;
        public bool NotSold { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string TargetDate { get; set; }
        public OrderProperty Property { get; set; }
    }
}