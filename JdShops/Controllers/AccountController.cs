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

        [HttpPatch("update/{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult UpdateUser([FromBody] UpdateUserDto dto, int  id)
        {
            _accountService.UpdateUser(dto, id);
            return Ok("User Updated Successfully");
        }

        [HttpPatch("resetPass/{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult ResetUserPassword([FromBody] UpdateUserDto dto , int id)
        {
            _accountService.PasswordReset(dto, id);
            return Ok("User Password Changed Successfully");
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult GetAllUsers([FromQuery] string searchPhrase)
        {
            var users =_accountService.GetAllUsers(searchPhrase);
            return Ok(users);
        }

        [HttpGet("users/{Id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public ActionResult GetUserById([FromRoute] int Id)
        {
            var users = _accountService.GetUserbyId(Id);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        
        public ActionResult DeleteUser([FromRoute] int id)
        {
            _accountService.DeleteUser(id);
            return NotFound();
        }

    }
}
