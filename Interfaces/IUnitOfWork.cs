using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository {get;}
        IProductRepository ProductRepository {get;}
        IOrderRepository OrderRepository {get;}
        IIngredientRepository IngredientRepository {get;}
        Task<bool> Complete();
    }
}