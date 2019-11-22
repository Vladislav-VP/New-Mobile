using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Api
{
    public class RequestEditDataUserApiView
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}
