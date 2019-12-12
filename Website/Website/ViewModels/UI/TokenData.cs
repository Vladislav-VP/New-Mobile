using System;

namespace ViewModels.Api
{
    public class TokenData
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpirationDate { get; set; }
    }
}
