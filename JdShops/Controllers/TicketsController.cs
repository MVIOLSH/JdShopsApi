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
    [Route("api/tickets")]
    [ApiController]
    [Authorize]

        public class TicketsController : ControllerBase
        {
            private readonly ITicketService _ticketService;

            public TicketsController(ITicketService ticketService)
            {
                _ticketService = ticketService;
            }
            [HttpPost]
            [Authorize(Roles = "Admin, AdvancedUser, User")]
            public ActionResult CreateTicket([FromBody]TicketsDto dto)
            {
                _ticketService.Create(dto);
                return Ok();
            }

            [HttpPatch("{id}")]
            [Authorize(Roles = "Admin, AdvancedUser")]
            public ActionResult UpdateTicket([FromBody] TicketsDto dto, [FromRoute] int id)
            {
                _ticketService.Update(dto, id);
                return Ok();
            }

            [HttpGet]
            [Authorize(Roles = "Admin, AdvancedUser")]
            public ActionResult<IEnumerable<TicketsDto>> GetAllTickets([FromQuery] string searchPhrase)
            {
                var allTicketsDtos = _ticketService.GetAll(searchPhrase);
                return Ok(allTicketsDtos);
            }

            [HttpGet("{id}")]
            [Authorize(Roles = "Admin, AdvancedUser")]
            public ActionResult<IEnumerable<TicketsDto>> GetById([FromRoute] int id)
            {
                var ticket = _ticketService.GetById(id);
                return Ok(ticket);
            }


        }
}
