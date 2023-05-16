using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Data;
using Prodaja_kruha_backend.Data.Repositories;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _unitOfWork.CustomerRepository.GetAllCustomers();
        }

        [HttpGet("{customerName}")]
        public async Task<ActionResult<Customer>> GetCustomerByName(string customerName)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomerByName(customerName);
            if(customer == null){return BadRequest($"There is no user with {customerName} name in the database");}
            return customer;
        }
    }
}