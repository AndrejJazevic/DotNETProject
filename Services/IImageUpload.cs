using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IImageUpload
    {
        public List<String> UploadImages(List<IFormFile> images);
    }
}
