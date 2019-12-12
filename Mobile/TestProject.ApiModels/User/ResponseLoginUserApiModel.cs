﻿using System;

namespace TestProject.ApiModels.User
{
    public class ResponseLoginUserApiModel
    {        
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpirationDate { get; set; }
    }
}
