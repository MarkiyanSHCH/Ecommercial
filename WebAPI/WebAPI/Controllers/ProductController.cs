using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
            return Ok(_productServices.Get(_configuration));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            return Ok(_productServices.GetById(_configuration, id));
        }

        [HttpGet("category/{id}")]
        public IActionResult GetByCategory(int id)
        {

            return Ok(_productServices.GetByCategory(_configuration, id));
        }
    }
}
