using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using JdShops.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.StaticFiles;

namespace JdShops.Controllers
{
    [Route( "files")]
    [Authorize]
    public class FileController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ShopsDBContext _dbContext;

        public FileController(IWebHostEnvironment environment, ShopsDBContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new []{"fileName"})]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/FILES/{fileName}";
            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist)
            {
                return NotFound();
            }

            var fileType = new FileExtensionContentTypeProvider();
            fileType.TryGetContentType(filePath, out string fileTypeResult);

            var fileContent= System.IO.File.ReadAllBytes(filePath);
            return File(fileContent,fileTypeResult, fileName);

        }

        [HttpPost]
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        public ActionResult UploadFile([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/FILES/{fileName}";

                var fileDbEntry = new Entities.File()
                {
                    Id = 0,
                    FileName = fileName,
                    FileType = "Document",
                    FilePath = fullPath,
                    Description = "test"
                };

                _dbContext.File.Add(fileDbEntry);
                _dbContext.SaveChanges();
                

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok();
            }

            return BadRequest();

        }
    }
}
