using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private string _userId => User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        private readonly IConfiguration _configuration;
        private readonly OrderServices _orderServices;

        public OrdersController(IConfiguration configuration, OrderServices orderServices)
            => (this._configuration, this._orderServices) = (configuration, orderServices);

        [HttpGet]
        public IActionResult GetOrders()
        {
            ProductList orders = new ProductList
            {
                Products = _orderServices.GetOrders(_configuration, _userId).ToList()
            };

            if (orders != null)
                return Ok(orders);

            return NotFound();
        }
    }
}
