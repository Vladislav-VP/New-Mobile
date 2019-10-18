namespace TestProject.Configurations
{
    public static class Constants
    {
        public const int MinPasswordLength = 6;

        public const string PasswordPattern = @"\w+";

        public static string RestUrl = "http://192.168.1.207:5000/api/todoitems/{0}";
    }
}
