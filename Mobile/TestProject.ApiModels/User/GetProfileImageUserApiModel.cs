namespace TestProject.ApiModels.User
{
    public class GetProfileImageUserApiModel
    {
        public string UserName { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
