using System;
using System.IO;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public byte[] DecryptBase64String(string encrtyptedString)
        {
            return Convert.FromBase64String(encrtyptedString);
        }

        public string GetEncryptedString(Stream stream)
        {
            string encryptedString = null;

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] decryptedImageString = memoryStream.ToArray();
                encryptedString = Convert.ToBase64String(decryptedImageString);
            }

            return encryptedString;
        }

        
    }
}
