using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ApiModels.User
{
    public class RequestLoginUserApiModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
