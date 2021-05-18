using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JdShops.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace JdShops.Services
{
    public interface IImageUploadService
    {
        bool PostImageToShop(IFormFile file, string shopNumber);
    }

    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool PostImageToShop(IFormFile file, string shopNumber)
        {
            try
            {
                var rootPath = _environment.WebRootPath;
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/MEDIA/{shopNumber}/{fileName}";
                var dirPath = $"{rootPath}/MEDIA/{shopNumber}/";

                if (file.Length > 0)
                {
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                        return true;
                    }
                }
                else
                {
                    return false ;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;
        }
    }
}
