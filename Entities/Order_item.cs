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
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}