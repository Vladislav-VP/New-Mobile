namespace Services.Interfaces
{
    public interface IImageService
    {
        void UploadImage(string imageUrl, byte[] imageBytes);

        byte[] GetImage(string imageUrl);
    }
}
