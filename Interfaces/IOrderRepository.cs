using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersFromUser(string customerName);
        Task<IEnumerable<OrderDTO>> GetOrdersFromUserWithOptions(string customerName, string options);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        Task<OrderDTO> UpdateOrder(OrderDTO orderDTO, int id);
        Task<Order_item> DeleteOrder(int id);
        Task<Order_item> CompleteOrder(int id);
    }
}