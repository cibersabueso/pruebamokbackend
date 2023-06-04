using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using NorthwindAPI.Repositories;
using System.Collections.Generic;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;

        public OrdersController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            _orderRepository.Create(order);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var existingOrder = _orderRepository.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.CustomerID = order.CustomerID;
            existingOrder.EmployeeID = order.EmployeeID;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.RequiredDate = order.RequiredDate;
            existingOrder.ShippedDate = order.ShippedDate;
            existingOrder.ShipVia = order.ShipVia;
            existingOrder.Freight = order.Freight;
            existingOrder.ShipName = order.ShipName;
            existingOrder.ShipAddress = order.ShipAddress;
            existingOrder.ShipCity = order.ShipCity;
            existingOrder.ShipRegion = order.ShipRegion;
            existingOrder.ShipPostalCode = order.ShipPostalCode;
            existingOrder.ShipCountry = order.ShipCountry;

            _orderRepository.Update(existingOrder);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var existingOrder = _orderRepository.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(existingOrder);
            return Ok();
        }
    }
}
