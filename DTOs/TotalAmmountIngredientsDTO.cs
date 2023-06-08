using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class TotalAmmountIngredientsDTO
    {
        public string IngredientType { get; set; }
        public float TotalIngredientWeight { get; set; }
        public float TotalIngredientPrice { get; set; }
    }
}