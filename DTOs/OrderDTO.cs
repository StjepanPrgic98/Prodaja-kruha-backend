using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public List<ProductInfoDTO> OrderItems { get; set; }
        public string TargetDay { get; set; }
        public bool Completed { get; set; }
    }
}