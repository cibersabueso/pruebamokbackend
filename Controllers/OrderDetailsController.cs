using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using NorthwindAPI.Repositories;
using System.Collections.Generic;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("api/orderdetails")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public OrderDetailsController(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return _orderDetailRepository.GetAll();
        }

        [HttpGet("{orderId}/{productId}")]
        public OrderDetail GetOrderDetailById(int orderId, int productId)
        {
            return _orderDetailRepository.GetById(orderId, productId);
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.Create(orderDetail);
            return Ok();
        }

        [HttpPut("{orderId}/{productId}")]
        public IActionResult UpdateOrderDetail(int orderId, int productId, OrderDetail orderDetail)
        {
            var existingOrderDetail = _orderDetailRepository.GetById(orderId, productId);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.Discount = orderDetail.Discount;

            _orderDetailRepository.Update(existingOrderDetail);
            return Ok();
        }

        [HttpDelete("{orderId}/{productId}")]
        public IActionResult DeleteOrderDetail(int orderId, int productId)
        {
            var existingOrderDetail = _orderDetailRepository.GetById(orderId, productId);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            _orderDetailRepository.Delete(existingOrderDetail);
            return Ok();
        }
    }
}
