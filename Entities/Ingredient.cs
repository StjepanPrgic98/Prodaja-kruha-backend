using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime Outdated { get; set; }
    }
}