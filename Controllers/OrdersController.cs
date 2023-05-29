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

        

        
    }
}