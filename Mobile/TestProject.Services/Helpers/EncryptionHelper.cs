using System;
using System.IO;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public byte[] DecryptBase64String(string encrtyptedString)
        {
            // TODO : Remove this method after refactoring conversion on iOS
            return Convert.FromBase64String(encrtyptedString);
        }

        public byte[] GetBytes(Stream stream)
        {
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();                
            }
            return bytes;
        }        
    }
}
