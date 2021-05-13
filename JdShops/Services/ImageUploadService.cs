using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JdShops.Models;
using Microsoft.AspNetCore.Hosting;

namespace JdShops.Services
{
    public interface IImageUploadService
    {
        Task<string> Post(Image objFileImage, string shopNumber);
    }

    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> Post(Image objFileImage, string shopNumber)
        {
            try
            {
                if (objFileImage.files.Length > 0)
                {
                    if (!Directory.Exists((_environment.WebRootPath + $"\\MEDIA\\{shopNumber}")))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\MEDIA\\" + shopNumber);
                    }

                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + $"\\MEDIA\\{shopNumber}\\" + objFileImage.files.FileName))
                    {
                        objFileImage.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return $"\\MEDIA\\{shopNumber}\\" + objFileImage.files.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return null;
        }
    }
}
