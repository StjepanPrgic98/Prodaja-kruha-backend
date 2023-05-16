using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers();
        Task<ActionResult<Customer>> GetCustomerByName(string customerName);                         
    }
}