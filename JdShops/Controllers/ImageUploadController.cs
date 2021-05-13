using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Models;
using JdShops.Services;
using Microsoft.AspNetCore.Hosting;

namespace JdShops.Controllers
{
    [Route("api/Images")]
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

        [HttpPost("{id}")]
        public ActionResult<Task<string>> Post([FromRoute] string id, [FromForm]Image objFile)
        {
            _imageUploadService.Post(objFile, id);
            return Ok(objFile);
        }
    }
}
