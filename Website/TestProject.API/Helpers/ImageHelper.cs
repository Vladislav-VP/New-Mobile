using Microsoft.AspNetCore.Http;
using System.IO;

namespace TestProject.API.Helpers
{
    public class ImageHelper
    {
        public byte[] GetImageBytes(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            byte[] imageBytes = null;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                imageBytes = stream.ToArray();
            }
            return imageBytes;
        }
    }
}
