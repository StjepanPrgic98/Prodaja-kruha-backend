using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}