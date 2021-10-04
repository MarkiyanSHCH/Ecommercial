using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private string UserId => User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        private readonly IConfiguration _configuration;
        private readonly OrderServices _services;

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
            _services = new OrderServices();
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public IActionResult GetOrders()
        {
            var orders = _services.GetOrders(_configuration, UserId);

            if(orders != null)
            {
                return Ok(orders);
            }

            return NotFound();
        }
    }
}
