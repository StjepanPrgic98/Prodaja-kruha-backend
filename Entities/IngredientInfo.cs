using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class IngredientInfo
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public float Percentage { get; set; }
    }
}