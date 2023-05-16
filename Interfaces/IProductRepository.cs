using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface IProductRepository
    {
        Task<ActionResult<IEnumerable<Product>>> GetAllProducts();
        Task<ActionResult<Product>> GetProductByType(string productType);
        Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(float productPrice);
    }
}