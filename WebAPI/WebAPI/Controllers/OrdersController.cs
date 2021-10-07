using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private string _userId => User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        private readonly IConfiguration _configuration;
        private readonly OrderServices _orderServices;

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
            _orderServices = new OrderServices();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            IEnumerable<Product> orders = _orderServices.GetOrders(_configuration, _userId);

            if (orders != null)
                return Ok(orders);

            return NotFound();
        }
    }
}
