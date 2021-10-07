using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ProductServices _productServices;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _productServices = new ProductServices();
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _productServices.Get(_configuration);

            if (products != null)
                return Ok(products);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _productServices.GetById(_configuration, id);

            if (product != null)
                return Ok(product);

            return NotFound();
        }

        [HttpGet("category/{id}")]
        public IActionResult GetByCategory(int id)
        {
            IEnumerable<Product> products = _productServices.GetByCategory(_configuration, id);

            if (products != null)
                return Ok(products);

            return NotFound();
        }
    }
}
