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
    public class ShopController : ControllerBase
    {
        private readonly ShopServices _shopServices;

        public ShopController(ShopServices shopServices)
            => this._shopServices = shopServices;

        [HttpGet]
        public IActionResult GetAll()
        {
            var shops = new GetShopList
            {
                Shops = this._shopServices.GetAll().ToList()
            };

            if (shops.Shops.Any()) return Ok(shops);

            return NotFound();
        }

        [HttpGet("{shopId}")]
        public IActionResult GetById([Required][FromQuery]int shopId)
        {
            if (shopId <= 0) return BadRequest();

            Shop shop = this._shopServices.GetById(shopId);

            if (shop != null) return Ok(shop);

            return NotFound();
        }
    }
}
