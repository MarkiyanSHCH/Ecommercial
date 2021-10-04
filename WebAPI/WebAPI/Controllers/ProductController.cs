using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ProductServices _services;

        public ProductController(IConfiguration configuration) 
        {  
            _configuration = configuration;
            _services = new ProductServices();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_services.Get(_configuration));
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            
            return Ok(_services.GetById(_configuration,id));
        }

        [HttpGet("category/{id}")]
        public IActionResult GetByCategory(int id)
        {

            return Ok(_services.GetByCategory(_configuration, id));
        }
    }
}
