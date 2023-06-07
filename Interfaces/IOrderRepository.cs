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
        Task<OrderDTO> GetOrderById(int id);
        Task<IEnumerable<OrderDTO>> GetAllOrdersWithOptions(string options);
        Task<IEnumerable<OrderDTO>> GetOrdersFromUserWithOptions(string customerName, string options);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<IEnumerable<OrderDTO>> GetAllOrdersForTargetDay(string day);
        Task<IEnumerable<OrderDTO>> GetOrdersForTargetDate(string date);
        Task<IEnumerable<TotalAmmoutDTO>> GetTotalAmmountOfProductsOrdered();
        Task<IEnumerable<TotalAmmoutDTO>> GetTotalAmmountOfProductsOrderedForTargetDate(string date);
        Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        Task<bool> IsUsernameTaken(string username);
        Task<OrderDTO> UpdateOrder(OrderDTO orderDTO, int id);
        Task<OrderDTO> GetOrderProperty(OrderDTO orderDTO);
        Task<OrderDTO> SetOrderProperty(int orderId, string property);
        Task<IEnumerable<OrderDTO>> GetOrdersWithPropertyRegular();
        Task<IEnumerable<OrderDTO>> GetListOfRegularOrdersWithOptions(string option, string date, string day);
        Task<OrderDTO> DeleteOrder(int id);
        Task<OrderDTO> CompleteOrder(int id);
        Task<OrderDTO> MarkAsNotSold(int id);
    }
}