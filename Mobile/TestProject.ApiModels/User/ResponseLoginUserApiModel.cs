﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ApiModels.User
{
    public class ResponseLoginUserApiModel
    {
        public int Id { get; set; }
        
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
