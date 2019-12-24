namespace ViewModels.Api.User
{
    public class ResponseChangeEmailUserApiView
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }
    }
}
