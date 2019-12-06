namespace TestProject.ApiModels.User
{
    public class RequestEditProfileImageUserApiModel
    {
        public string Id { get; set; }

        public byte[] ImageBytes { get; set; }

        public string ImageUrl { get; set; }
    }
}
