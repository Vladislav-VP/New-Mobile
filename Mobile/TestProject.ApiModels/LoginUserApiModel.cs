using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ApiModels
{
    public class LoginUserApiModel : BaseApiModel
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
