using System;

namespace TestProject.ApiModels.User
{
    public class ResponseRefreshAccessTokenUserApiModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public DateTime TokenExpirationDate { get; set; }
    }
}
