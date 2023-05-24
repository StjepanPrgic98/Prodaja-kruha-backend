using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public int Quantity { get; set; }
    }
}