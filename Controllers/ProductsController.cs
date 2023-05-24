using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Data;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;
using SQLitePCL;
using System.Security.Cryptography;

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

        [HttpGet("type/{productType}")]
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

        [HttpPost("create")]
        public async Task<ActionResult<Product>> CreateProduct(ProductDTO productDTO)
        {
            var product = await _unitOfWork.ProductRepository.AddProduct(productDTO);
            if(product == null){return BadRequest();}
            return product;
        }

        [HttpDelete("delete/{type}")]
        public async Task<ActionResult<Product>> DeleteProduct(string type)
        {
            var productToDelete = await _unitOfWork.ProductRepository.DeleteProduct(type);
            if(productToDelete == null){return BadRequest($"{type} product does not exist!");}
            return productToDelete;
        }

        [HttpPut("update")]
        public async Task<ActionResult<Product>> UpdateProduct(ProductDTO productDTO)
        {
            var productToUpdate = await _unitOfWork.ProductRepository.UpdateProduct(productDTO);
            if(productToUpdate == null){return BadRequest("This product does not exist!");}
            return productToUpdate;
        }
    }
}