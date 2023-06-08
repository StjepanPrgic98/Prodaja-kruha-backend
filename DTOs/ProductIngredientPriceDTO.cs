using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class ProductIngredientPriceDTO
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public List<ProductIngredientPercentageDTO> IngredientInfo { get; set; }
        public float TotalProductPrice { get; set; }
    }
}