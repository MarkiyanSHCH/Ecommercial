using System.Linq;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Core.Domain.Orders;
using Core.Domain.Auth;

using WebAPI.Models.Api.Orders;

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
        {
            this._orderServices = orderServices;
            this._authServices = authServices;
        }

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
            if (request == null)
                return BadRequest();

            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized();

            int result = this._orderServices
                            .AddOrderProduct(
                                userId,
                                this._authServices.GetUserEmail(base.User),
                                request.ShopId,
                                request.TotalPrice,
                                request.OrderLines.Select(x => x.ToDomainModel()));

            if (result != 0)
                return Ok(result);

            return BadRequest();
        }
    }
}