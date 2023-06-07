using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {  
           return await _context.Customers.ToListAsync();
        }

        public async Task<ActionResult<Customer>> GetCustomerByName(string customerName)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Name == customerName);   
            if(customer == null){return null;}
            return customer;
        }

        public async Task<CustomerDTO> SetCustomerProperty(string customerName, string property)
        {
            var customerProperties = await _context.CustomerProperties
            .Include(cp => cp.Customer)
            .Where(cp => cp.Customer.Name == customerName)
            .FirstOrDefaultAsync();

            if(customerProperties == null){return null;}

            customerProperties.Property = property;
            await _context.SaveChangesAsync();

            
            CustomerDTO customerDTO = new CustomerDTO
            {
                CustomerName = customerProperties.Customer.Name,
                Property = customerProperties.Property
            };

            return customerDTO;
           
        }
    }
}