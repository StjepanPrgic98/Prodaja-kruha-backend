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
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<Order_item> CompleteOrder(int id)
        {
            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .Where(oi => oi.Orders.Id == id).SingleOrDefaultAsync();
            if(order == null){return null;}
            order.Completed = true;
            return order;
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            List<ProductInfoDTO> orderItems = new List<ProductInfoDTO>(orderDTO.OrderItems);
            List<Product> products = new List<Product>();
            List<int> productsQuantity = new List<int>();
            List<ProductInfo> productsInfo = new List<ProductInfo>();

            for (int i = 0; i < orderItems.Count; i++)
            {
                products[i] = await _context.Products.FirstOrDefaultAsync(x => x.Type == orderItems[i].ProductType);
                productsInfo[i].Products = products;
                productsInfo[i].Quantity = orderItems[i].Quantity;
            }

            var order = new Order
            {
                Customers = new Customer {Name = orderDTO.CustomerName}
            };

            for (int i = 0; i < orderItems.Count; i++)
            {
                productsInfo[i] = new ProductInfo
                {
                    Products = products,
                    Quantity = productsQuantity[i]
                };
            }
            
            

            var orderItem = new Order_item
            {
                ProductsInfo = productsInfo,
                TargetDay = orderDTO.TargetDay,
                Orders = order
            };

            _context.Orders.Add(order);
            _context.Order_Items.Add(orderItem);
            return orderDTO;
        }

        public async Task<Order_item> DeleteOrder(int id)
        {
            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .Where(oi => oi.Orders.Id == id).SingleOrDefaultAsync();

            _context.Order_Items.RemoveRange(order);
            return order;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(pi => new ProductInfoDTO
                {
                    ProductType = string.Join(", ", pi.Products.Select(x => x.Type)),
                    Quantity = pi.Quantity
                }).ToArray(),
                
                TargetDay = oi.TargetDay,
                Completed = oi.Completed
            })
            .ToListAsync();

            if(orders.Count < 1){return null;}

            return orders; 
        }


        public async Task<IEnumerable<OrderDTO>> GetAllOrdersFromUser(string customerName)
        {
            var orders = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .Where(oi => oi.Orders.Customers.Name == customerName)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(pi => new ProductInfoDTO
                {
                    ProductType = pi.Products.Select(x => x.Type).ToString(),
                    Quantity = pi.Quantity
                }).ToArray(),

                TargetDay = oi.TargetDay,
                Completed = oi.Completed
            })
            
            .ToListAsync();

            if(orders.Count < 1){return null;}

            return orders;         
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersFromUserWithOptions(string customerName, string options)
        {
            bool orderCompleted = false;
            if(options == "completed"){orderCompleted = true;}
            if(options != "completed" && options != "notCompleted"){return null;}
            var orders = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .Where(oi => oi.Orders.Customers.Name == customerName && oi.Completed == orderCompleted)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(pi => new ProductInfoDTO
                {
                    ProductType = pi.Products.Select(x => x.Type).ToString(),
                    Quantity = pi.Quantity
                }).ToArray(),

                TargetDay = oi.TargetDay,
                Completed = oi.Completed
            })
            
            .ToListAsync();

            if(orders.Count < 1){return null;}

            return orders;       
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO orderDTO, int id)
        {    
            List<ProductInfoDTO> orderItems = new List<ProductInfoDTO>(orderDTO.OrderItems);
            List<Product> products = new List<Product>();
            List<int> productsQuantity = new List<int>();
            List<ProductInfo> productsInfo = new List<ProductInfo>();

            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo).ThenInclude(pi => pi.Products)
            .Where(oi => oi.Orders.Customers.Id == id)         
            .SingleOrDefaultAsync();

            for (int i = 0; i < orderItems.Count; i++)
            {
                products[i] = await _context.Products.FirstOrDefaultAsync(x => x.Type == orderItems[i].ProductType);
                productsInfo[i] = await _context.ProductsInformation.FirstOrDefaultAsync(x => x.Products == products);
            }
            
        

            order.Orders.Customers.Name = orderDTO.CustomerName;
            order.ProductsInfo = productsInfo;
            order.TargetDay = orderDTO.TargetDay;

            return orderDTO;
        }
    }
}