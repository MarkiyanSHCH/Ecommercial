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
        private readonly OrderServices _orderServices;
        private readonly AuthServices _authServices;

        public OrdersController(OrderServices orderServices, AuthServices authServices)
            => (this._orderServices, this._authServices) = (orderServices, authServices);


        [HttpGet]
        public IActionResult GetOrders()
        {
            int userId = this._authServices.GetUserId(base.User);
            if (userId == 0)
                return Unauthorized();

            var orders = new GetOrdersList
            {
                Orders = this._orderServices.GetAllOrders(userId).ToList()
            };

            if (orders.Orders.Any())
                return Ok(orders);

            return NotFound();
        }

        [HttpGet("{orderId}/lines")]
        public IActionResult GetOrderLines([Required] int orderId)
        {
            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized();

            if (orderId <= 0)
                return BadRequest();

            var orderLines = new GetOrderLinesList
            {
                OrderLines = this._orderServices.GetAllOrderLines(orderId).ToList()
            };

            if (orderLines.OrderLines.Any())
                return Ok(orderLines);

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] PostOrderRequest request)
        {
            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized();

            if (request != null)
                return Ok(
                    this._orderServices
                        .AddOrderProduct(
                            userId,
                            request.ShopId,
                            request.TotalPrice,
                            request.OrderLines)
                        );

            return BadRequest();
        }
    }
}