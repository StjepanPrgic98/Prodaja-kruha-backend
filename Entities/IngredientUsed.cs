using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class IngredientUsed
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ICollection<IngredientInfo> IngredientsInfo { get; set; }    
    }
}