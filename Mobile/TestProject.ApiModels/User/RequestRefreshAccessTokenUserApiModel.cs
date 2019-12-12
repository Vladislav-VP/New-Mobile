using System;

namespace TestProject.ApiModels.User
{
    public class RequestRefreshAccessTokenUserApiModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpirationDate { get; set; }
    }
}
