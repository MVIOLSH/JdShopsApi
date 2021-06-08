using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdShops.Entities;
using JdShops.Models;
using JdShops.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace JdShops.Controllers
{
    [Route("api/images")]
    [Authorize]
    [ApiController]

    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ShopsDBContext _dbContext;

        public ImageUploadController(IWebHostEnvironment environment, ShopsDBContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;

        }

        //Shop Image Controller Methods
        #region
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpPost("{id}")]
        public ActionResult PostImageToShop([FromRoute] string id, [FromForm] IFormFile file, [FromForm] string description)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/MEDIA/{id}/{fileName}";
                var dirPath = $"{rootPath}/MEDIA/{id}/";

                if (file != null && file.Length > 0)
                {
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    var imgShopCheckList = _dbContext.ImgShop.Where(c => c.ShopNumber == id).ToList();
                    foreach (var check in imgShopCheckList)
                    {
                        if (check.FileName == fileName)
                        {
                            return BadRequest("File Already Exist");
                        }
                    }

                    var fileDbEntry = new Entities.ImgShop()
                    {
                        Id = 0,
                        FileName = fileName,
                        FilePath = fullPath,
                        Description = description,
                        ShopNumber = id
                    };

                    _dbContext.ImgShop.Add(fileDbEntry);
                    _dbContext.SaveChanges();


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("File Uploaded");
                }
                else
                {
                    return BadRequest("The file seems to be empty");
                }
            }

            return BadRequest("File Error");

        }

        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpGet("{shopNumber}")]
        public ActionResult<List<ImgShop>> GetListOfImages([FromRoute] string shopNumber)
        {
            var list = _dbContext.ImgShop.Where(c => c.ShopNumber == shopNumber).ToList();

            return Ok(list);
        }

        
        [AllowAnonymous]
        [HttpGet("{shopNumber}/{fileName}")]
        public ActionResult GetImage([FromRoute] string shopNumber, string fileName)
        {

            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/MEDIA/{shopNumber}/{fileName}";
            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist)
            {
                return NotFound();
            }

            var fileType = new FileExtensionContentTypeProvider();
            fileType.TryGetContentType(filePath, out string fileTypeResult);

            var fileContent = System.IO.File.ReadAllBytes(filePath);
            return File(fileContent, fileTypeResult, fileName);
        }

        [Authorize(Roles = "Admin, AdvancedUser")]
        [HttpDelete("{id}")]
        public ActionResult<List<ImgShop>> DeleteImage([FromRoute] string id)
        {
            var idInt = int.Parse(id);
            var file = _dbContext.ImgShop.Where(c => c.Id == idInt).ToList();

            foreach (var item in file)
            {
                if (!System.IO.File.Exists(item.FilePath))
                {
                    return BadRequest();
                }
                System.IO.File.Delete(item.FilePath);
                _dbContext.Remove(item);
                _dbContext.SaveChanges();
            }         

            return Ok();
        }

        #endregion

        #region
        //Additional Address Image Controller Methods
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpPost("additional/{id}")]
        public ActionResult PostImageAddress([FromRoute] string id, [FromForm] IFormFile file, [FromForm] string description)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/MEDIA/additional/{id}/{fileName}";
                var dirPath = $"{rootPath}/MEDIA/additional/{id}/";

                if (file != null && file.Length > 0)
                {
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    var imgShopCheckList = _dbContext.ImgShop.Where(c => c.ShopNumber == id).ToList();
                    foreach (var check in imgShopCheckList)
                    {
                        if (check.FileName == fileName)
                        {
                            return BadRequest("File Already Exist");
                        }
                    }

                    var fileDbEntry = new Entities.ImgAdditionalAddress()
                    {
                        Id = 0,
                        FileName = fileName,
                        FilePath = fullPath,
                        Description = description,
                        ShopNumber = id
                    };

                    _dbContext.ImgAdditionalAddresses.Add(fileDbEntry);
                    _dbContext.SaveChanges();


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("File Uploaded");
                }
                else
                {
                    return BadRequest("The file seems to be empty");
                }
            }
            return BadRequest("File Error");

        }

        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpGet("additional/{shopNumber}")]
        public ActionResult<List<ImgShop>> GetListOfImagesAdditionalAddress([FromRoute] string shopNumber)
        {
            var list = _dbContext.ImgAdditionalAddresses.Where(c => c.ShopNumber == shopNumber).ToList();

            return Ok(list);
        }

        
        [AllowAnonymous]
        [HttpGet("additional/{shopNumber}/{fileName}")]
        public ActionResult GetAdditionalAddressImage([FromRoute] string shopNumber, string fileName)
        {

            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/MEDIA/additional/{shopNumber}/{fileName}";
            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist)
            {
                return NotFound();
            }

            var fileType = new FileExtensionContentTypeProvider();
            fileType.TryGetContentType(filePath, out string fileTypeResult);

            var fileContent = System.IO.File.ReadAllBytes(filePath);
            return File(fileContent, fileTypeResult, fileName);
        }

        [Authorize(Roles = "Admin, AdvancedUser")]
        [HttpDelete("/additional/{id}")]
        public ActionResult<List<ImgShop>> DeleteImageAdditionalAddress([FromRoute] string id)
        {
            var idInt = int.Parse(id);
            var file = _dbContext.ImgAdditionalAddresses.Where(c => c.Id == idInt).ToList();

            foreach (var item in file)
            {
                if (!System.IO.File.Exists(item.FilePath))
                {
                    return BadRequest();
                }
                System.IO.File.Delete(item.FilePath);
                _dbContext.Remove(item);
                _dbContext.SaveChanges();
            }

            return Ok();
        }

        #endregion

        #region
        //Tickets Image Controller Methods
        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpPost("ticket/{id}")]
        public ActionResult PostImageTicket([FromRoute] string id, [FromForm] IFormFile file, [FromForm] string description)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/MEDIA/ticket/{id}/{fileName}";
                var dirPath = $"{rootPath}/MEDIA/ticket/{id}/";

                if (file != null && file.Length > 0)
                {
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    var imgShopCheckList = _dbContext.ImgTickets.Where(c => c.Id == int.Parse(id)).ToList();
                    foreach (var check in imgShopCheckList)
                    {
                        if (check.FileName == fileName)
                        {
                            return BadRequest("File Already Exist");
                        }
                    }

                    var fileDbEntry = new Entities.ImgTickets()
                    {
                        Id = 0,
                        FileName = fileName,
                        FilePath = fullPath,
                        Description = description,
                        TicketId = int.Parse(id)
                    };

                    _dbContext.ImgTickets.Add(fileDbEntry);
                    _dbContext.SaveChanges();


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("File Uploaded");
                }
                else
                {
                    return BadRequest("The file seems to be empty");
                }
            }
            return BadRequest("File Error");

        }

        [Authorize(Roles = "Admin, AdvancedUser, VerifiedUser")]
        [HttpGet("additional/{shopNumber}")]
        public ActionResult<List<ImgShop>> GetListOfImagesTickets([FromRoute] string ticketId)
        {
            var list = _dbContext.ImgTickets.Where(c => c.Id == int.Parse(ticketId)).ToList();

            return Ok(list);
        }


        [AllowAnonymous]
        [HttpGet("additional/{shopNumber}/{fileName}")]
        public ActionResult GetTicketImage([FromRoute] string ticketId, string fileName)
        {

            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/MEDIA/additional/{ticketId}/{fileName}";
            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist)
            {
                return NotFound();
            }

            var fileType = new FileExtensionContentTypeProvider();
            fileType.TryGetContentType(filePath, out string fileTypeResult);

            var fileContent = System.IO.File.ReadAllBytes(filePath);
            return File(fileContent, fileTypeResult, fileName);
        }

        [Authorize(Roles = "Admin, AdvancedUser")]
        [HttpDelete("/additional/{id}")]
        public ActionResult<List<ImgShop>> DeleteImageTicket([FromRoute] string imageId)
        {
            var idInt = int.Parse(imageId);
            var file = _dbContext.ImgTickets.Where(c => c.Id == idInt).ToList();

            foreach (var item in file)
            {
                if (!System.IO.File.Exists(item.FilePath))
                {
                    return BadRequest();
                }
                System.IO.File.Delete(item.FilePath);
                _dbContext.Remove(item);
                _dbContext.SaveChanges();
            }

            return Ok();
        }

        #endregion


    }
}


