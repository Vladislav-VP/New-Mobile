using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ApiModels.User
{
    public class RequestEditProfileImageUserApiModel
    {
        public int Id { get; set; }

        public byte[] ImageBytes { get; set; }

        public string ImageUrl { get; set; }
    }
}
