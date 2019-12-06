namespace TestProject.ApiModels.User
{
    public class GetProfileImageUserApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
