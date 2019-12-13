namespace ViewModels.Api.User
{
    public class GetProfileImageUserApiView
    {
        public string UserName { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
