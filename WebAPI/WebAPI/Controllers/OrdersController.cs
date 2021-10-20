using System;
using System.Linq;
using System.Security.Claims;

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
            ProductList orders = new ProductList
            {
                Products = this._orderServices.GetOrders(_userId).ToList()
            };

            if (orders != null) return Ok(orders);

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] int ProductId)
        { 
            if (_userId == 0) return Unauthorized();

            if (ProductId != 0)
            {
                this._orderServices.AddOrderProduct(_userId, ProductId);
                return Ok(ProductId);
            }
            return BadRequest();
        }

        [HttpDelete("{ProductId:int}")]
        public IActionResult RemoveOrderItem([FromRoute] int ProductId)
        {
            if (ProductId != 0)
            {
                this._orderServices.RemoveOrderProduct(_userId, ProductId);
                return NoContent();
            }
            return BadRequest();
        }
    }
}