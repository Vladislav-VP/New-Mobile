using System.IO;

using Services.Interfaces;

namespace Services
{
    public class ImageService : IImageService
    {
        public void UploadImage(string imageUrl, byte[] imageBytes)
        {
            using (var imageStream = new MemoryStream(imageBytes))
            {
                using (var imageFileStream = new FileStream(imageUrl, FileMode.Create))
                {
                    imageStream.CopyTo(imageFileStream);
                }
            }
        }

        public byte[] GetImage(string imageUrl)
        {
            byte[] imageBytes = null;
            using (var imageFileStream = new FileStream(imageUrl, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var imageMemoryStream = new MemoryStream())
                {
                    imageFileStream.CopyTo(imageMemoryStream);
                    imageBytes = imageMemoryStream.ToArray();
                }
            }
            return imageBytes;
        }
    }
}
