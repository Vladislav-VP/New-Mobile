using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Configurations
{
    public static class Constants
    {
        public const string DatabaseName = "TestProjectDB.db";

        public const string LocalStorageName = "UserData.xml";

        public const int MinPasswordLength = 6;

        public const string InvalidPasswordCharacterPattern = @"\W";

        public const int ToastDuration = 3000;
    }
}
