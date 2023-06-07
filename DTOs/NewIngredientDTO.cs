using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.DTOs
{
    public class NewIngredientDTO
    {
        public int Id { get; set; }
        public string IngredientType { get; set; }
        public float IngredientPackageWeight { get; set; }
        public float IngredientPrice { get; set; }   
    }
}