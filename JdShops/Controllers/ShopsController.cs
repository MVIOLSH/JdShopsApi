using AutoMapper;
using JdShops.Entities;
using JdShops.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Services;

namespace JdShops.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IShopsService _shopsService;
        public ShopsController(IShopsService shopsService)
        {
            _shopsService = shopsService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody]UpdateShopDto dto, [FromRoute] int id )
        {
            _shopsService.ShopUpdate(id, dto);
          return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
          _shopsService.ShopDelete(id);
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateShop([FromBody] CreateShopDto dto)
        {
         var id = _shopsService.Create(dto);
         return Created($"/api/shops/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShopsDto>> GetAll()
        {
            var shopsDtos = _shopsService.GetAll();
            return Ok(shopsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ShopsDto> Get([FromRoute] int id)
        {
            var shop = _shopsService.GetById(id);
            return Ok(shop);
        }

    }
}
