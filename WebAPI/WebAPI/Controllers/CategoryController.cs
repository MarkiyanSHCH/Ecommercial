using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly CategoryServices _services;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _services = new CategoryServices();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_services.Get(_configuration));
        }

    }
}
