using System.IO;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IEncryptionHelper
    {
        byte[] GetBytes(Stream stream);
    }
}
