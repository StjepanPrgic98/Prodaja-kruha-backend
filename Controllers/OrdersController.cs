using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Data;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrders();
            if(orders == null){return BadRequest("Something went wrong");}
            return Ok(orders);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderById(id);
            if(order == null){return BadRequest("Order doesnt exist!");}
            return order;
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersForTargetDate(string date)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersForTargetDate(date);
            if(orders == null){return BadRequest("No orders for this date!");}
            return Ok(orders);
        }
        
        [HttpGet("targetDay/{day}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersForTargetDay(string day)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrdersForTargetDay(day);
            if(orders == null){return BadRequest("Something went wrong!");}
            return Ok(orders);
        }

        [HttpGet("{customerName}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersFromUser(string customerName)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrdersFromUser(customerName);
            if(orders == null){return BadRequest("This user does not have orders!");}
            return Ok(orders);
        }
        
        [HttpGet("options/{options}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersWithOptions(string options)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrdersWithOptions(options);
            if(orders == null){return BadRequest("Something went wrong!");}
            return Ok(orders);
        }

        [HttpGet("{customerName}/{options}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersFromUserWithOptions(string customerName, string options)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersFromUserWithOptions(customerName, options);
            if(orders == null){return BadRequest("Something went wrong!");}
            return Ok(orders);
        }

        [HttpGet("totalAmmount")]
        public async Task<ActionResult<IEnumerable<TotalAmmoutDTO>>> GetTotalAmmountOfProductsOrdered()
        {
            var totalAmmount = await _unitOfWork.OrderRepository.GetTotalAmmountOfProductsOrdered();
            return Ok(totalAmmount);
        }

        [HttpGet("totalAmmount/{date}")]
        public async Task<ActionResult<IEnumerable<TotalAmmoutDTO>>> GetTotalAmmountOfProductsOrderedForTargetDate(string date)
        {
            var totalAmmount = await _unitOfWork.OrderRepository.GetTotalAmmountOfProductsOrderedForTargetDate(date);
            if(totalAmmount == null){return BadRequest("Something went wrong!");}
            return Ok(totalAmmount);
        }

        [HttpGet("properties")]
        public async Task<ActionResult<OrderDTO>> GetOrderByProperty(OrderDTO orderDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderProperty(orderDTO);
            if(order == null){return BadRequest("This order does not exist!");}
            return order;
        }

        [HttpGet("listOfRegularOrders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersWithPropertyRegular()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersWithPropertyRegular();
            if(orders == null){return BadRequest("Could not get orders!");}
            return Ok(orders);
        }

        [HttpPost("listOfRegularOrders/{option}/{date}/{day}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetListOfRegularOrdersWithOptions(string option, string date, string day)
        {
            var orders = await _unitOfWork.OrderRepository.GetListOfRegularOrdersWithOptions(option, date, day);
            if(orders == null){return BadRequest("Something went wrong!");}
            return Ok(orders);
        }

        [HttpPost("create")]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var order = await _unitOfWork.OrderRepository.CreateOrder(orderDTO);
            if(order == null){return BadRequest("Something went wrong");}
            return order;
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(OrderDTO orderDTO, int id)
        {
            var order = await _unitOfWork.OrderRepository.UpdateOrder(orderDTO, id);
            if(order == null){return BadRequest("Something went wrong!");}
            return Ok(order);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<OrderDTO>> DeleteOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.DeleteOrder(id);
            if(order == null){return BadRequest("Something went wrong!");}
            await _unitOfWork.Complete();
            return Ok(order);
        }

        [HttpPatch("complete/{id}")]
        public async Task<ActionResult<OrderDTO>> CompleteOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.CompleteOrder(id);
            if(order == null){return BadRequest("Order does not exist!");}
            await _unitOfWork.Complete();
            return order;
        }

        [HttpPatch("property/{orderId}/{property}")]
        public async Task<ActionResult<OrderDTO>> SetOrderProperty(int orderId, string property)
        {
            var order = await _unitOfWork.OrderRepository.SetOrderProperty(orderId, property);
            if(order == null){return BadRequest("This order does not exist!");}
            return order;
        }

        [HttpPatch("notSold/{id}")]
        public async Task<ActionResult<OrderDTO>> MarkAsNotSold(int id)
        {
            var order = await _unitOfWork.OrderRepository.MarkAsNotSold(id);
            if(order == null){return BadRequest("Order does not exist!");}
            return order;
        }

        

        
    }
}