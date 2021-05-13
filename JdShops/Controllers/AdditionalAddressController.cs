using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JdShops.Models;
using JdShops.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JdShops.Controllers
{
    [Route("api/shops/{shopNumber}/additional")]
    [ApiController]
    [Authorize]

    public class AdditionalAddressController : ControllerBase
    {
        private readonly IAdditionalAddressService _addressService;

        public AdditionalAddressController(IAdditionalAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "AdvancedUser")]
        public ActionResult Post([FromRoute] int shopNumber,[FromBody] AddAdditionalAddressDto dto)
        {
            var newAddress =  _addressService.Create(shopNumber, dto);
            return Created($"api/{shopNumber}/additional/{newAddress}", null);
        }

        [HttpGet]
        public ActionResult<AddAdditionalAddressDto> GetAllForThisShop([FromRoute] int shopNumber)
        {
            var additionals = _addressService.GetAllAdditionalForShop(shopNumber);
            return Ok(additionals);
        }

        [HttpGet("{id}")]
        public ActionResult<AddAdditionalAddressDto> GetAdditionalAddressById([FromRoute] int shopNumber, [FromRoute] int id)
        {
            var additional = _addressService.GetAdditionalAddressById(shopNumber, id);
            return Ok(additional);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "AdvancedUser")]
        public ActionResult Delete([FromRoute] int shopNumber, [FromRoute] int id)
        {
            _addressService.AdditionalAddressDelete(id, shopNumber);
            return NotFound();
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "AdvancedUser")]
        public ActionResult Update([FromRoute] int shopNumber, [FromRoute] int id, [FromBody] AddAdditionalAddressDto dto)
        {
            _addressService.AdditionalAddressUpdate(shopNumber, id, dto);
            return Ok();
        }


    }
}
