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
        private string _userId => User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        private readonly OrderServices _orderServices;

        public OrdersController(OrderServices orderServices)
            => (this._orderServices) = (orderServices);

        [HttpGet]
        public IActionResult GetOrders()
        {
            ProductList orders = new ProductList
            {
                Products = _orderServices.GetOrders(_userId).ToList()
            };

            if (orders != null)
                return Ok(orders);

            return NotFound();
        }
    }
}