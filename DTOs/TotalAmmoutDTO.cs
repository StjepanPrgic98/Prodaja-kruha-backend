using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class TotalAmmoutDTO
    {
        public string ProductType { get; set; }
        public int TotalQuantity { get; set; }
        public float TotalPrice { get; set; }
    }
}