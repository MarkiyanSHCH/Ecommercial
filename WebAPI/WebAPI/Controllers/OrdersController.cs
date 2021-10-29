using System;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Core.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private int _userId => Convert.ToInt32(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        private readonly OrderServices _orderServices;

        public OrdersController(OrderServices orderServices)
            => this._orderServices = orderServices;

        [HttpGet]
        public IActionResult GetOrders()
        {
            if (this._userId == 0) return Unauthorized();

            var orders = new GetOrdersList
            {
                Orders = this._orderServices.GetAllOrders(_userId).ToList()
            };

            if (orders.Orders.Any()) return Ok(orders);

            return NotFound();
        }

        [HttpGet("{orderId}/lines")]
        public IActionResult GetOrderLines([Required] int orderId)
        {
            if (this._userId > 0) return Unauthorized();
            if (orderId > 0) return BadRequest();

            var orderLines = new GetOrderLinesList
            {
                OrderLines = this._orderServices.GetAllOrderLines(orderId).ToList()
            };

            if (orderLines.OrderLines.Any()) return Ok(orderLines);

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] PostOrderRequest request)
        {
            if (this._userId > 0) return Unauthorized();

            if (request != null)
                return Ok(
                    this._orderServices
                        .AddOrderProduct(
                            _userId,
                            request.ShopId,
                            request.TotalPrice,
                            request.OrderLines)
                        );

            return BadRequest();
        }
    }
}