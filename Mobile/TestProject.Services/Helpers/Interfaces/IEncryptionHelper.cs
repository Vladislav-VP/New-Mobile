using System.IO;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IEncryptionHelper
    {
        string GetEncryptedString(Stream stream);

        byte[] DecryptBase64String(string encrtyptedString);
    }
}
