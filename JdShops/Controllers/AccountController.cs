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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;

        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult UpdateUser([FromBody] RegisterUserDto dto, int  id)
        {
            _accountService.UpdateUser(dto, id);
            return Ok("User Updated Successfully");
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult GetAllUsers([FromQuery] string searchPhrase)
        {
            var users =_accountService.GetAllUsers(searchPhrase);
            return Ok(users);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            _accountService.DeleteUser(id);
            return NotFound();
        }

    }
}
