using System;

namespace ViewModels.Api.User
{
    public class ResponseLoginUserApiView
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpirationDate { get; set; }
    }
}
