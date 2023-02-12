using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ImageUpload : IImageUpload
    {
        private readonly Cloudinary _cloudinary;
        public ImageUpload(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public List<String> UploadImages(List<IFormFile> images)
        {
            List<String> urls = new List<String>();

            foreach (IFormFile image in images)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.Name, image.OpenReadStream())
                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                urls.Add(uploadResult.SecureUrl.ToString());
            }

            return urls;
        }
    }
}
