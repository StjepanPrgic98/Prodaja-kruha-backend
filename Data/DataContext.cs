using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProperties> CustomerProperties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_item> Order_Items { get; set; }
        public DbSet<OrderProperty> OrderProperties { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientUsed> IngredientsUsed { get; set; }
        public DbSet<IngredientInfo> IngredientInfos { get; set; }
        
        
        
    }
}