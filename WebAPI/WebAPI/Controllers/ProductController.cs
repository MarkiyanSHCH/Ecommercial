using System.Linq;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Core.Services;
using WebAPI.Models;
using Domain.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductController(ProductServices productServices)
            => this._productServices = productServices;

        [HttpGet]
        public IActionResult Get()
        {
            var products = new GetProductList
            {
                Products = this._productServices.Get().ToList()
            };

            if (products.Products.Any()) 
                return Ok(products);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([Required] int id)
        {
            if (id <= 0) 
                return BadRequest();

            Product product = this._productServices.GetById(id);

            if (product != null) 
                return Ok(product);

            return NotFound();
        }

        [HttpGet("category/{id}")]
        public IActionResult GetByCategory([Required] int id)
        {
            if (id <= 0) 
                return BadRequest();

            var products = new GetProductList
            {
                Products = this._productServices.GetByCategory(id).ToList()
            };

            if (products.Products.Any()) 
                return Ok(products);

            return NotFound();
        }
    }
}