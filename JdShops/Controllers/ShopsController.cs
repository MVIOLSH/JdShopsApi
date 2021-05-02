using AutoMapper;
using JdShops.Entities;
using JdShops.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Controllers
{
    [Route("api/shops")]
    public class ShopsController : ControllerBase
    {
        private readonly ShopsDBContext _dbContext;
        private readonly IMapper _mapper;
        public ShopsController(ShopsDBContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateShop([FromBody] CreateShopDto dto)
        {
            var shop = _mapper.Map<Shops>(dto);
            _dbContext.Shops.Add(shop);
            _dbContext.SaveChanges();

            return Created($"/api/shops/{shop.ShopNumber}", null);

        }

        [HttpGet]
        public ActionResult<IEnumerable<ShopsDto>> GetAll()
        {
           var shops = _dbContext
                .Shops
                .Include( r => r.Address)
                .ToList();

            var shopsDtos = _mapper.Map<List<ShopsDto>>(shops);  

            return Ok(shopsDtos);

        }

        [HttpGet("{id}")]
        public ActionResult<ShopsDto> Get([FromRoute] int id)
        {
            var shop = _dbContext
                .Shops
                .Include(r=>r.Address)
                .FirstOrDefault(r => r.ShopNumber == id);

            if (shop is null)
            {
                return NotFound();
            }
            else
            {
                var shopDto = _mapper.Map<ShopsDto>(shop);
                return Ok(shopDto);
            }
        }

    }
}
