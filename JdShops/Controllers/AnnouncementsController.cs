using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Models;
using JdShops.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JdShops.Controllers
{
    public class AnnouncementsController
    {
        [Route("api/announcements")]
        [ApiController]
        [Authorize]
        public class AnnouncementsController : ControllerBase
        {
            private readonly AnnouncementsService.IAnnouncementsService _announcementsService;

            public AnnouncementsController(AnnouncementsService.IAnnouncementsService announcementsService)
            {
                _announcementsService = announcementsService;
            }

            [HttpPut("{id}")]
            [Authorize(Roles = "Admin, AdvancedUser")]

            public ActionResult Update([FromBody] AnnouncementsDto dto, [FromRoute] int id)
            {
                _announcementsService.AnnouncementUpdate(id, dto);
                return Ok();
            }

            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin, AdvancedUser")]
            public ActionResult Delete([FromRoute] int id)
            {
                _announcementsService.AnnouncementDelete(id);
                return NotFound();
            }

            [HttpPost]
            [Authorize(Roles = "Admin, AdvancedUser")]
            public ActionResult CreateAnnouncement([FromBody] AnnouncementsDto dto)
            {
                var id = _announcementsService.Create(dto);
                return Created($"/api/announcements/{id}", null);
            }

            [HttpGet]
            [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser, DummyUser")]
            public ActionResult<IEnumerable<AnnouncementsDto>> GetAll()
            {
                var anncouncementsDtos = _announcementsService.GetAll();
                return Ok(anncouncementsDtos);
            }

            [HttpGet("{id}")]
            [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser, DummyUser")]
            public ActionResult<AnnouncementsDto> Get([FromRoute] int id)
            {
                var shop = _announcementsService.GetById(id);
                return Ok(shop);
            }

        }
    }
}
