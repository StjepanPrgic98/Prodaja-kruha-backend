using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;
using SQLitePCL;

namespace Prodaja_kruha_backend.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;   
        }
        public async Task<Product> AddProduct(ProductDTO productDTO)
        {
            if(productDTO == null){return null;}
            
            var product = new Product
            {
                Type = productDTO.Type,
                Price = productDTO.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProduct(string type)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Type == type);
            if(productToDelete == null){return null;}
            _context.Remove(productToDelete);
            await _context.SaveChangesAsync();
            return productToDelete;
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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Type == productType);
            if(product == null){return null;}
            return product;
        }

        public async Task<Product> UpdateProduct(ProductDTO productDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Type == productDTO.Type);
            if(product == null){return null;}
            product.Price = productDTO.Price;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}