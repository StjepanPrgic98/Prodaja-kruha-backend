using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;   
        }
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(float productPrice)
        {
            var products = await _context.Products.Where(x => x.Price == productPrice).ToListAsync();
            if(products.Count < 1){return null;}
            return products;
        }

        public async Task<ActionResult<Product>> GetProductByType(string productType)
        {
            var products = await _context.Products.FirstOrDefaultAsync(x => x.Type == productType);
            if(products == null){return null;}
            return products;
        }
    }
}