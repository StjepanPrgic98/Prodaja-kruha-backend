using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersFromUser(string customerName);
        Task<IEnumerable<OrderDTO>> GetAllOrdersWithOptions(string options);
        Task<IEnumerable<OrderDTO>> GetOrdersFromUserWithOptions(string customerName, string options);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<IEnumerable<TotalAmmoutDTO>> GetTotalAmmountOfProductsOrdered();
        Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        Task<OrderDTO> UpdateOrder(OrderDTO orderDTO, int id);
        Task<OrderDTO> DeleteOrder(int id);
        Task<OrderDTO> CompleteOrder(int id);
    }
}