using System.Linq;

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
            GetShopList shops = new GetShopList
            {
                Shops = _shopServices.GetAll().ToList()
            };

            if (shops != null) return Ok(shops);

            return NotFound();
        }

        //TODO : Think about how to correct get id
        /*[HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Shop shop = _shopServices.GetById(id);

            if (shop != null) return Ok(shop);

            return NotFound();
        }*/
    }
}
