using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class IngredientsDTO
    {
        public string ProductType { get; set; }
        public List<IngredientInfoDTO> IngredientsInfo { get; set; }

    }
}