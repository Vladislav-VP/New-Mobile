namespace ViewModels.Api.User
{
    public class ResponseRefreshAccessTokenUserApiView
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
