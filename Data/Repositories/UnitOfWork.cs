using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public ICustomerRepository CustomerRepository => new CustomerRepository(_context);

        public IProductRepository ProductRepository =>  new ProductRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}