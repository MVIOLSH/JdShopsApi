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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JdShops.Controllers
{
    [Route("api/shops")]
    [ApiController]
    [Authorize]
    public class ShopsController : ControllerBase
    {
        private readonly IShopsService _shopsService;
        public ShopsController(IShopsService shopsService)
        {
            _shopsService = shopsService;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        
        public ActionResult Update([FromBody]UpdateShopDto dto, [FromRoute] int id )
        {
            _shopsService.ShopUpdate(id, dto);
          return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult Delete([FromRoute] int id)
        {
          _shopsService.ShopDelete(id);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult CreateShop([FromBody] CreateShopDto dto)
        {
         var id = _shopsService.Create(dto);
         return Created($"/api/shops/{id}", null);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser, DummyUser")]
        public ActionResult<IEnumerable<ShopsDto>> GetAll([FromQuery] string searchPhrase)
        {
            var shopsDtos = _shopsService.GetAll(searchPhrase);
            return Ok(shopsDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser, DummyUser")]
        public ActionResult<IEnumerable<ShopsDto>> Get([FromRoute] int id)
        {
            var shop = _shopsService.GetById(id);
            return Ok(shop);
        }

    }
}
