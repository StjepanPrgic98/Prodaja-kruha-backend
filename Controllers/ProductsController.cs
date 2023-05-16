using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Data;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;
using SQLitePCL;

namespace Prodaja_kruha_backend.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduts()
        {
            return await _unitOfWork.ProductRepository.GetAllProducts();
        }

        [HttpGet("{productType}")]
        public async Task<ActionResult<Product>> GetProductByType(string productType)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByType(productType);

            if(product == null){return BadRequest($"There is no product of type {productType} in the database");}

            return product;
        }

        [HttpGet("price/{productPrice}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(float productPrice)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByPrice(productPrice);
            if(product == null){return BadRequest($"There is no product with price {productPrice} in the database");}
            return product;
        }
    }
}