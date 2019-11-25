namespace ViewModels.Api.User
{
    public class GetProfileImageUserApiView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
