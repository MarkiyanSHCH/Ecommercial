using System.Linq;

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

        public ProductController(IConfiguration configuration, ProductServices productServices)
            => (this._configuration, this._productServices) = (configuration, productServices);

        [HttpGet]
        public IActionResult Get()
        {
            ProductList products = new ProductList 
            { 
                Products = _productServices.Get(_configuration).ToList()
            };
                

            if (products != null)
                return Ok(products);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _productServices.GetById(_configuration, id);

            if (product != null) return Ok(product);

            return NotFound();
        }

        [HttpGet("category/{id}")]
        public IActionResult GetByCategory(int id)
        {
            ProductList products = new ProductList
            {
                Products = _productServices.GetByCategory(_configuration, id).ToList()
            };

            if (products != null) return Ok(products);

            return NotFound();
        }
    }
}
