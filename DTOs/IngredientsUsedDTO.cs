using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class IngredientsUsedDTO
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public List<IngredientInfoDTO> IngredientTypes { get; set; }
    }
}