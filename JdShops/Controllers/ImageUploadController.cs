using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Models;
using JdShops.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace JdShops.Controllers
{
    [Route("api/images")]
    [Authorize]
    [ApiController]

    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly IImageUploadService _imageUploadService;

        public ImageUploadController(IWebHostEnvironment environment, IImageUploadService imageUploadService)
        {
            _environment = environment;
            _imageUploadService = imageUploadService;
        }
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpPost("{id}")]
        public ActionResult Post([FromRoute] string id, [FromForm]IFormFile file)
        {
            _imageUploadService.PostImageToShop(file, id);
            return Ok();
        }

        
    }

}
