using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class ProductIngredientPercentageDTO
    {
        public string IngredientType { get; set; }
        public float IngredientPercentageWeigth { get; set; }
        public float IngredientPercentagePrice { get; set; }
    }
}