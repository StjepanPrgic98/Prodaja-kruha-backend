using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customers { get; set; }
    }
}