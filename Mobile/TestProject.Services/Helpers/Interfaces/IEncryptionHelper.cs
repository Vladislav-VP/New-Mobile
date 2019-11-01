using System.IO;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IEncryptionHelper
    {
        byte[] DecryptBase64String(string encrtyptedString);

        byte[] GetBytes(Stream stream);
    }
}
