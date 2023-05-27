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

        public async Task<OrderDTO> CompleteOrder(int id)
        {
            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .ThenInclude(oi => oi.Product)
            .Where(oi => oi.Orders.Id == id).FirstOrDefaultAsync();
            if(order == null){return null;}
            order.Completed = true;

            var orderDTO = new OrderDTO
            {
                OrderId = order.Id,
                CustomerName = order.Orders.Customers.Name,
                OrderItems = order.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),
                TargetDay = order.TargetDay,
                Completed = order.Completed
            };
            return orderDTO;
            
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            List<ProductInfoDTO> orderItems = new List<ProductInfoDTO>(orderDTO.OrderItems);
            List<ProductInfo> productsInfo = new List<ProductInfo>();

            for (int i = 0; i < orderItems.Count; i++)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(x => x.Type == orderItems[i].ProductType);
                ProductInfo productInfo = new ProductInfo
                {
                    Product = product,
                    Quantity = orderItems[i].Quantity
                };

                productsInfo.Add(productInfo);
               
            }
            
            var order = new Order
            {
                Customers = new Customer {Name = orderDTO.CustomerName}
            };

        
            var orderItem = new Order_item
            {
                ProductsInfo = productsInfo,
                TargetDay = orderDTO.TargetDay,
                Orders = order
            };

            _context.Orders.Add(order);
            _context.Order_Items.Add(orderItem);
                      
            await _context.SaveChangesAsync();
            
            orderDTO.OrderId = orderItem.Id;
            return orderDTO;
        }

        public async Task<OrderDTO> DeleteOrder(int id)
        {
            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .ThenInclude(oi => oi.Product)
            .Where(oi => oi.Orders.Id == id).FirstOrDefaultAsync();

            _context.Order_Items.RemoveRange(order);
            
            OrderDTO orderDTO = new OrderDTO
            {
                OrderId = order.Id,
                CustomerName = order.Orders.Customers.Name,
                OrderItems = order.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),
                TargetDay = order.TargetDay,
                Completed = order.Completed
            };
            return orderDTO;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .ThenInclude(oi => oi.Product)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),
                
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

                OrderItems = oi.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),

                TargetDay = oi.TargetDay,
                Completed = oi.Completed
            })
            
            .ToListAsync();

            if(orders.Count < 1){return null;}

            return orders;         
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersWithOptions(string options)
        {
            bool orderCompleted = false;
            if(options == "completed"){orderCompleted = true;}
            if(options != "completed" && options != "notCompleted"){return null;}
            var orders = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo)
            .ThenInclude(oi => oi.Product)
            .Where(oi => oi.Completed == orderCompleted)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),

                TargetDay = oi.TargetDay,
                Completed = oi.Completed
            })
            
            .ToListAsync();

            if(orders.Count < 1){return null;}

            return orders;       
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
            .ThenInclude(oi => oi.Product)
            .Where(oi => oi.Orders.Customers.Name == customerName && oi.Completed == orderCompleted)
            .Select(oi => new OrderDTO
            {
                OrderId = oi.Orders.Id,
                CustomerName = oi.Orders.Customers.Name,

                OrderItems = oi.ProductsInfo.Select(x => new ProductInfoDTO
                {
                    ProductType = x.Product.Type,
                    Quantity = x.Quantity
                }).ToList(),

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
            List<ProductInfo> productsInfos = new List<ProductInfo>();

            var order = await _context.Order_Items
            .Include(oi => oi.Orders)
            .ThenInclude(o => o.Customers)
            .Include(oi => oi.ProductsInfo).ThenInclude(pi => pi.Product)
            .Where(oi => oi.Orders.Customers.Id == id)         
            .SingleOrDefaultAsync();

            for (int i = 0; i < orderItems.Count; i++)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(x => x.Type == orderItems[i].ProductType);

                ProductInfo productInfo = new ProductInfo
                {
                    Product = product,
                    Quantity = orderItems[i].Quantity
                };
                productsInfos.Add(productInfo);
                
                    
            
            }
            
            order.Orders.Customers.Name = orderDTO.CustomerName;
            order.ProductsInfo = productsInfos;
            order.TargetDay = orderDTO.TargetDay;

            await _context.SaveChangesAsync();
            orderDTO.OrderId = order.Id;

            return orderDTO;
        }
    }
}